using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

internal interface IController {

    void Run();
    void Idle();
    void Death();
    void Enable();
    void Disable();

}

public class PlayerController : BaseController, IController {

    private IStageController stageManager;
    [SerializeField] private Collider2D conflictChecker;
    [SerializeField] private SpriteRenderer playerHip;
    [SerializeField] private SpriteAtlas hipAtlas;
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private ParticleSystem mainJet;
    [SerializeField] private ParticleSystem subJet;
    [SerializeField] private BoxCollider2D playerCol;

    [SerializeField]
    private Vector2 currentColSize, crouchColSize, currentColOffset, crouchColOffset;

    [SerializeField] private float speed_us;
    [SerializeField] private float jetTimeLimit;
    [SerializeField] private ContactFilter2D contactFilterGround;


    private ContactFilter2D contactFilterWall;
    private List<Collider2D> conflictResult = new List<Collider2D>();
    private Vector2 moveBuffer, jumpBuffer, jumpOffset, jetBuffer;
    private Vector3 hipPositionBuffer;
    private float crouchingTime;
    private float onaraAmount;
    private bool isCrouching;
    private IReadOnlyList<Sprite> hips;

    private InputAction pcAction;

    enum PlayerState {
        Normal = 0,
        Crouch = 1,
        CrouchLv1 = 2,
        CrouchLv2 = 3,
        CrouchLv3 = 4
    }


    private void Start() {
        stageManager = GameObject.FindWithTag("System").GetComponent<IStageController>();
        InitPlayer();
        stageManager.Initialize();
    }


    private void Update() {
        if (jetControl.wasPressedThisFrame) JetStart();
        if (jetControl.isPressed) Jet();
        if (jetControl.wasReleasedThisFrame) JetEnd();
        if (isCrouching) CountCrouch();
        if (conflictChecker.OverlapCollider(contactFilterWall, conflictResult) >
            0) Death();
    }


    private void InitPlayer() {
        //プレイヤーのスプライト登録
        List<Sprite> _hips = new List<Sprite>();
        _hips.Add(hipAtlas.GetSprite("hip"));
        _hips.Add(hipAtlas.GetSprite("hip_crouch"));
        _hips.Add(hipAtlas.GetSprite("hip_lv1"));
        _hips.Add(hipAtlas.GetSprite("hip_lv2"));
        _hips.Add(hipAtlas.GetSprite("hip_lv3"));
        hips = _hips.AsReadOnly();

        //調整用オフセット
        jumpBuffer = new Vector2(0f, 100f);
        jumpOffset = new Vector2(0f, 250f);
        jetBuffer = new Vector2(0f, 1800f);

        //キー入力時のコールバック
        jumpAction.performed += Jump;
        crouchAction.performed += Crouch;
        respawnAction.performed += ctx => Death();

        //判定サイズのキャッシュ
        currentColSize = playerCol.size;
        currentColOffset = playerCol.offset;

        //プレイヤーの相対座標のキャッシュ　初期化用
        hipPositionBuffer = transform.localPosition;
        
        stageManager.SetTweenSpeed(speed_us);
    }


    public void Jump(InputAction.CallbackContext ctx) {
        if (rig.IsTouching(contactFilterGround)) {
            rig.AddForce(jumpBuffer * Mathf.Pow(crouchingTime, 2) + jumpOffset,
                ForceMode2D.Impulse);
        }

        crouchingTime = 0f;
        isCrouching = false;
        playerHip.sprite = hips[(int) PlayerState.Normal];
        playerCol.size = currentColSize;
        playerCol.offset = currentColOffset;
    }


    public void JetStart() {
        mainJet.Play();
        subJet.Play();
    }


    public void Jet() {
        SetOnara(-Time.deltaTime);
        rig.AddForce(jetBuffer, ForceMode2D.Force);
    }


    public void JetEnd() {
        mainJet.Stop();
        subJet.Stop();
    }


    public void SetOnara(float t) {
        onaraAmount -= t;
        //TODO UpdateOnaraMetor
    }


    private void Crouch(InputAction.CallbackContext ctx) {
        playerHip.sprite = hips[(int) PlayerState.Crouch];
        playerCol.size = crouchColSize;
        playerCol.offset = crouchColOffset;
        isCrouching = true;
    }


    private void CountCrouch() {
        crouchingTime += Time.deltaTime;

        if (crouchingTime.InRange(0f, 1f)) {
            playerHip.sprite = hips[(int) PlayerState.CrouchLv1];
        }
        else if (crouchingTime.InRange(1f, 2f)) {
            playerHip.sprite = hips[(int) PlayerState.CrouchLv2];
        }
        else if (crouchingTime.InRange(2f, 3f)) {
            playerHip.sprite = hips[(int) PlayerState.CrouchLv3];
        }
        else if (crouchingTime > 3f) {
            Death();
        }
    }


    public void Run() {
        Enable();
        canMove = true;
        stageManager.Play();
        rig.simulated = true;
    }


    public void Idle() {
        Disable();
        canMove = false;
        stageManager.Pause();
        rig.simulated = false;
    }


    public void Death() {
        stageManager.Restart();
        rig.velocity = Vector2.zero;
        rig.simulated = true;
        transform.localPosition = hipPositionBuffer;

        playerHip.sprite = hips[(int) PlayerState.Normal];
        isCrouching = false;
        crouchingTime = 0f;

#if UNITY_EDITOR
        ExtentionMethods.ClearLog();
#endif
    }
}
