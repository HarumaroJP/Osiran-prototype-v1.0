// GENERATED AUTOMATICALLY FROM 'Assets/ScriptableObjects/InputSystem.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputSystem : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSystem"",
    ""maps"": [
        {
            ""name"": ""PC"",
            ""id"": ""8f1edcfa-d9fd-4d3d-9cbf-5752936d8893"",
            ""actions"": [
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""38f3472e-3237-4872-b1e4-0c6901c30577"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""202cc9b3-64ad-43e6-8de8-a09d5babbc99"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Respawn"",
                    ""type"": ""Button"",
                    ""id"": ""54577801-f15e-4d8a-9149-575c290ad618"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cc6844fc-2b20-4b54-8cc4-8562ed25017e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84a5137c-5d2b-4b30-a86f-02077c505f67"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be6849b8-07d1-4fdd-af36-6226e7bc235a"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Respawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PC
        m_PC = asset.FindActionMap("PC", throwIfNotFound: true);
        m_PC_Crouch = m_PC.FindAction("Crouch", throwIfNotFound: true);
        m_PC_Jump = m_PC.FindAction("Jump", throwIfNotFound: true);
        m_PC_Respawn = m_PC.FindAction("Respawn", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PC
    private readonly InputActionMap m_PC;
    private IPCActions m_PCActionsCallbackInterface;
    private readonly InputAction m_PC_Crouch;
    private readonly InputAction m_PC_Jump;
    private readonly InputAction m_PC_Respawn;
    public struct PCActions
    {
        private @InputSystem m_Wrapper;
        public PCActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Crouch => m_Wrapper.m_PC_Crouch;
        public InputAction @Jump => m_Wrapper.m_PC_Jump;
        public InputAction @Respawn => m_Wrapper.m_PC_Respawn;
        public InputActionMap Get() { return m_Wrapper.m_PC; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PCActions set) { return set.Get(); }
        public void SetCallbacks(IPCActions instance)
        {
            if (m_Wrapper.m_PCActionsCallbackInterface != null)
            {
                @Crouch.started -= m_Wrapper.m_PCActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_PCActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_PCActionsCallbackInterface.OnCrouch;
                @Jump.started -= m_Wrapper.m_PCActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PCActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PCActionsCallbackInterface.OnJump;
                @Respawn.started -= m_Wrapper.m_PCActionsCallbackInterface.OnRespawn;
                @Respawn.performed -= m_Wrapper.m_PCActionsCallbackInterface.OnRespawn;
                @Respawn.canceled -= m_Wrapper.m_PCActionsCallbackInterface.OnRespawn;
            }
            m_Wrapper.m_PCActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Respawn.started += instance.OnRespawn;
                @Respawn.performed += instance.OnRespawn;
                @Respawn.canceled += instance.OnRespawn;
            }
        }
    }
    public PCActions @PC => new PCActions(this);
    public interface IPCActions
    {
        void OnCrouch(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnRespawn(InputAction.CallbackContext context);
    }
}
