using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    protected InputAction jumpAction, crouchAction, respawnAction;
    protected bool canMove, inputUpSpace, inputDownSpace;

    protected void Awake()
    {
        crouchAction = playerInput.currentActionMap["Crouch"];
        jumpAction = playerInput.currentActionMap["Jump"];
        respawnAction = playerInput.currentActionMap["Respawn"];
    }

    protected virtual void Update()
    {
        //new input system

        //legacy input system
        // inputDownSpace = Input.GetKeyDown(KeyCode.Space);
        // inputUpSpace = Input.GetKeyUp(KeyCode.Space);
    }

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
