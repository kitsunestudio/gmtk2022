//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputController/TopDownPlayerController.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @TopDownPlayerController : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @TopDownPlayerController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TopDownPlayerController"",
    ""maps"": [
        {
            ""name"": ""Character"",
            ""id"": ""1e106df4-5ae2-436f-84b4-0c1304eb6d79"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""5529f91c-e2f0-4f50-b82a-edcf73f92c9b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""2ccc1055-cfc9-4487-8891-8b9e352fbc0c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b6942a7f-afdb-44a7-b4f3-16798a8637bb"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a3b6f0e7-f84c-4786-aafc-0658758d3d79"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""81f744e8-2fb4-45c5-ad51-2c7b85468066"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b4b4dbed-e2f5-4ba1-a180-33d6fd5f543c"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""86c3dc5e-097b-40c3-931d-94eff473a224"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ce149b91-233e-4066-99a9-7c93d2a1440f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3d4f1bd1-b06a-446e-b988-e56299cec520"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2d902b13-7e60-4041-b6bf-c16829fb6310"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""66bc936b-aeb9-4340-afd1-d488802fcd26"",
            ""actions"": [
                {
                    ""name"": ""OpenCloseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""a698d038-ccf0-4053-add0-33d3712719aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""938afcd2-e8af-43d9-8576-16273dbebe5e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenCloseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Character
        m_Character = asset.FindActionMap("Character", throwIfNotFound: true);
        m_Character_Movement = m_Character.FindAction("Movement", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_OpenCloseMenu = m_Menu.FindAction("OpenCloseMenu", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Character
    private readonly InputActionMap m_Character;
    private ICharacterActions m_CharacterActionsCallbackInterface;
    private readonly InputAction m_Character_Movement;
    public struct CharacterActions
    {
        private @TopDownPlayerController m_Wrapper;
        public CharacterActions(@TopDownPlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Character_Movement;
        public InputActionMap Get() { return m_Wrapper.m_Character; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterActions instance)
        {
            if (m_Wrapper.m_CharacterActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_CharacterActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public CharacterActions @Character => new CharacterActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_OpenCloseMenu;
    public struct MenuActions
    {
        private @TopDownPlayerController m_Wrapper;
        public MenuActions(@TopDownPlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @OpenCloseMenu => m_Wrapper.m_Menu_OpenCloseMenu;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @OpenCloseMenu.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnOpenCloseMenu;
                @OpenCloseMenu.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnOpenCloseMenu;
                @OpenCloseMenu.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnOpenCloseMenu;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OpenCloseMenu.started += instance.OnOpenCloseMenu;
                @OpenCloseMenu.performed += instance.OnOpenCloseMenu;
                @OpenCloseMenu.canceled += instance.OnOpenCloseMenu;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    public interface ICharacterActions
    {
        void OnMovement(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnOpenCloseMenu(InputAction.CallbackContext context);
    }
}
