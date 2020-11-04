using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class BaseController : MonoBehaviour {

    [SerializeField] private PlayerInput playerInput;
    protected InputAction jumpAction, crouchAction, respawnAction, jetAction;
    protected ButtonControl jetControl;
    protected bool canMove;


    protected void Awake() {
        //new input system
        crouchAction = playerInput.currentActionMap["Crouch"];
        jumpAction = playerInput.currentActionMap["Jump"];
        respawnAction = playerInput.currentActionMap["Respawn"];
        jetAction = playerInput.currentActionMap["Jet"];

        jetControl = (ButtonControl) jetAction.controls[0];
    }


    public void Enable() {
        playerInput.enabled = true;
    }
            

    public void Disable() {
        playerInput.enabled = false;
    }


    // protected virtual void Update() {
    //      legacy input system
    //      inputDownSpace = Input.GetKeyDown(KeyCode.Space);
    //      inputUpSpace = Input.GetKeyUp(KeyCode.Space);
    // }


    //     [SerializeField] private GUIStyle jumpUpStyle;
    //
    // #if UNITY_EDITOR
    //     private void OnGUI()
    //     {
    //         GUI.Box(new Rect(50, 50, 20, 20), "jumpUp : " + jumpAction.triggered, jumpUpStyle);
    //         GUI.Box(new Rect(50, 200, 20, 20), "jumpDown : " + crouchAction.triggered, jumpUpStyle);
    //     }
    // #endif
}
