using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

internal interface IController
{
    void Run();
    void Idle();
    void Death();
}

public class PlayerController : BaseController, IController
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform goal;
    [SerializeField] private Transform start;
    [SerializeField] private Collider2D conflictChecker;
    [SerializeField] private SpriteRenderer playerHip;
    [SerializeField] private Sprite[] hips;
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private float castDistance;
    [SerializeField] private float speed;
    [SerializeField] private float fieldLength;
    [SerializeField] private ContactFilter2D contactFilterGround;


    private ContactFilter2D contactFilterWall;
    private List<Collider2D> conflictResult = new List<Collider2D>();
    private Vector2 moveBuffer;
    private Vector2 jumpBuffer;
    private Vector3 hipPositionBuffer;
    private float crouchingTime;
    private bool isCrouching;

    private Tween cameraTween;
    private InputAction pcAction;

    enum PlayerState
    {
        Normal = 0,
        Crouch = 1,
        CrouchLv1 = 2,
        CrouchLv2 = 3,
        CrouchLv3 = 4
    }

    private void Start()
    {
        Run();
        PlayerInitialize();
    }

    protected override void Update()
    {
        // Debug.Log("isCrouching : " + isCrouching);
        // Debug.Log("inputUpSpace : " + inputUpSpace);
        if (jumpAction.triggered) Jump();
        if (crouchAction.triggered) Crouch();
        if (isCrouching) CountCrouch();
        if (conflictChecker.OverlapCollider(contactFilterWall, conflictResult) > 0) Death();
        if (respawnAction.triggered) Death();
        base.Update();
    }

    private void PlayerInitialize()
    {
        jumpBuffer = new Vector2(0f, 300f) * 1.5f;
        cameraTween = mainCamera.DOLocalMoveX(goal.localPosition.x, 15f).SetEase(Ease.Linear);
        hipPositionBuffer = transform.localPosition;
    }

    private int jumpCount;

    public void Jump()
    {
        if (rig.IsTouching(contactFilterGround))
        {
            rig.AddForce(jumpBuffer * crouchingTime, ForceMode2D.Impulse);
        }

        crouchingTime = 0f;
        isCrouching = false;
        playerHip.sprite = hips[(int) PlayerState.Normal];
        jumpCount++;
        Debug.Log("Jump" + jumpCount);
    }

    private void Crouch()
    {
        playerHip.sprite = hips[(int) PlayerState.Crouch];
        isCrouching = true;
    }

    private void CountCrouch()
    {
        crouchingTime += Time.deltaTime;

        if (crouchingTime.InRange(0f, 1f))
        {
            playerHip.sprite = hips[(int) PlayerState.CrouchLv1];
        }
        else if (crouchingTime.InRange(1f, 2f))
        {
            playerHip.sprite = hips[(int) PlayerState.CrouchLv2];
        }
        else if (crouchingTime.InRange(2f, 3f))
        {
            playerHip.sprite = hips[(int) PlayerState.CrouchLv3];
        }
        else if (crouchingTime > 3f)
        {
            Death();
        }
    }

    public void Run()
    {
        cameraTween.Play();
        canMove = true;
    }

    public void Idle()
    {
        canMove = false;
    }

    public void Death()
    {
        transform.localPosition = hipPositionBuffer;
        cameraTween.Restart();
        playerHip.sprite = hips[(int) PlayerState.Normal];
        isCrouching = false;
        crouchingTime = 0f;
        jumpCount = 0;
#if UNITY_EDITOR
        ExtentionMethods.ClearLog();
#endif
    }
}