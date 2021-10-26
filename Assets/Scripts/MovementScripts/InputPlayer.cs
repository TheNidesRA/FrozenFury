// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/MovementScripts/InputPlayer.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputPlayer : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputPlayer()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputPlayer"",
    ""maps"": [
        {
            ""name"": ""PlayerMain"",
            ""id"": ""30f32aa0-9b03-43d9-9b4f-62e56cc940bd"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""bb179efb-485e-4296-b1fa-f10b18415a22"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""88fa6d85-f582-494b-b351-c0c54ed13668"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""beed9f21-de11-4920-aff8-938712b48fb9"",
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
                    ""id"": ""8171d922-40f2-4ccc-a066-d0fe47e48ac8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""149cadff-e550-4dcc-981d-7df42fb53341"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""303ac0d8-219c-4067-ac76-ae68dff0eb7a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d2651b46-4c7f-4472-889d-040343c5b87f"",
                    ""path"": ""<Keyboard>/d"",
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
            ""name"": ""Building"",
            ""id"": ""82fea9c8-5f1f-44f2-bbfc-848ac6de4845"",
            ""actions"": [
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""d7d1023e-4558-49d6-b61e-afdf14184e03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RigthClick"",
                    ""type"": ""Button"",
                    ""id"": ""bf6d748b-8077-442a-9671-789235776a75"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Build1"",
                    ""type"": ""Value"",
                    ""id"": ""d2b11efd-9f83-48fa-9869-9aedc1cc2154"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Build2"",
                    ""type"": ""Button"",
                    ""id"": ""bb29c8cd-001f-48ef-b29b-cceb4b28700e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Build3"",
                    ""type"": ""Button"",
                    ""id"": ""2ab20cf1-be57-49d9-ae32-b251971a1962"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UndoSelection"",
                    ""type"": ""Button"",
                    ""id"": ""82f90a24-ed80-41a3-a16a-e3643ea6b708"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""1cae2784-3615-493d-8a70-3373861ba5ad"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""c6374032-8395-4b6e-aebf-7d7991db4285"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""157b92bc-4cd0-4725-b5d0-a219daacd8a5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a9f29be-fea8-4f5b-bb7f-7a70965a5080"",
                    ""path"": ""<Touchscreen>/touch0/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""834687a8-59ae-4359-a5fc-4cb2c4743d14"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79530066-a43d-4af3-abd9-922406743ada"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17950585-a4b0-4b03-8cfc-8d9ecaecf177"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""523bd1d4-e26d-4842-9711-f948f4834cd0"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UndoSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b236b662-6b95-49f0-92d7-aa2af4029aa5"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7a3083f-e6b9-408e-8568-3455844d473e"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccadb63e-f035-429e-8e16-0f897b6040a9"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RigthClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53b0e4c0-0be4-4173-a9df-36d3e99fc117"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Test"",
            ""id"": ""fc47b34b-bbd2-49f1-ac95-aa7b069dc28f"",
            ""actions"": [
                {
                    ""name"": ""SpawnEnemy"",
                    ""type"": ""Button"",
                    ""id"": ""f97beb4f-1e52-453a-b5ee-21fca05c7781"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""051e1830-60d2-48f0-9416-a5e5283e435e"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpawnEnemy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMain
        m_PlayerMain = asset.FindActionMap("PlayerMain", throwIfNotFound: true);
        m_PlayerMain_Movement = m_PlayerMain.FindAction("Movement", throwIfNotFound: true);
        // Building
        m_Building = asset.FindActionMap("Building", throwIfNotFound: true);
        m_Building_LeftClick = m_Building.FindAction("LeftClick", throwIfNotFound: true);
        m_Building_RigthClick = m_Building.FindAction("RigthClick", throwIfNotFound: true);
        m_Building_Build1 = m_Building.FindAction("Build1", throwIfNotFound: true);
        m_Building_Build2 = m_Building.FindAction("Build2", throwIfNotFound: true);
        m_Building_Build3 = m_Building.FindAction("Build3", throwIfNotFound: true);
        m_Building_UndoSelection = m_Building.FindAction("UndoSelection", throwIfNotFound: true);
        m_Building_MousePosition = m_Building.FindAction("MousePosition", throwIfNotFound: true);
        m_Building_Rotate = m_Building.FindAction("Rotate", throwIfNotFound: true);
        // Test
        m_Test = asset.FindActionMap("Test", throwIfNotFound: true);
        m_Test_SpawnEnemy = m_Test.FindAction("SpawnEnemy", throwIfNotFound: true);
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

    // PlayerMain
    private readonly InputActionMap m_PlayerMain;
    private IPlayerMainActions m_PlayerMainActionsCallbackInterface;
    private readonly InputAction m_PlayerMain_Movement;
    public struct PlayerMainActions
    {
        private @InputPlayer m_Wrapper;
        public PlayerMainActions(@InputPlayer wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMain_Movement;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMain; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMainActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMainActions instance)
        {
            if (m_Wrapper.m_PlayerMainActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMainActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_PlayerMainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public PlayerMainActions @PlayerMain => new PlayerMainActions(this);

    // Building
    private readonly InputActionMap m_Building;
    private IBuildingActions m_BuildingActionsCallbackInterface;
    private readonly InputAction m_Building_LeftClick;
    private readonly InputAction m_Building_RigthClick;
    private readonly InputAction m_Building_Build1;
    private readonly InputAction m_Building_Build2;
    private readonly InputAction m_Building_Build3;
    private readonly InputAction m_Building_UndoSelection;
    private readonly InputAction m_Building_MousePosition;
    private readonly InputAction m_Building_Rotate;
    public struct BuildingActions
    {
        private @InputPlayer m_Wrapper;
        public BuildingActions(@InputPlayer wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftClick => m_Wrapper.m_Building_LeftClick;
        public InputAction @RigthClick => m_Wrapper.m_Building_RigthClick;
        public InputAction @Build1 => m_Wrapper.m_Building_Build1;
        public InputAction @Build2 => m_Wrapper.m_Building_Build2;
        public InputAction @Build3 => m_Wrapper.m_Building_Build3;
        public InputAction @UndoSelection => m_Wrapper.m_Building_UndoSelection;
        public InputAction @MousePosition => m_Wrapper.m_Building_MousePosition;
        public InputAction @Rotate => m_Wrapper.m_Building_Rotate;
        public InputActionMap Get() { return m_Wrapper.m_Building; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BuildingActions set) { return set.Get(); }
        public void SetCallbacks(IBuildingActions instance)
        {
            if (m_Wrapper.m_BuildingActionsCallbackInterface != null)
            {
                @LeftClick.started -= m_Wrapper.m_BuildingActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_BuildingActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_BuildingActionsCallbackInterface.OnLeftClick;
                @RigthClick.started -= m_Wrapper.m_BuildingActionsCallbackInterface.OnRigthClick;
                @RigthClick.performed -= m_Wrapper.m_BuildingActionsCallbackInterface.OnRigthClick;
                @RigthClick.canceled -= m_Wrapper.m_BuildingActionsCallbackInterface.OnRigthClick;
                @Build1.started -= m_Wrapper.m_BuildingActionsCallbackInterface.OnBuild1;
                @Build1.performed -= m_Wrapper.m_BuildingActionsCallbackInterface.OnBuild1;
                @Build1.canceled -= m_Wrapper.m_BuildingActionsCallbackInterface.OnBuild1;
                @Build2.started -= m_Wrapper.m_BuildingActionsCallbackInterface.OnBuild2;
                @Build2.performed -= m_Wrapper.m_BuildingActionsCallbackInterface.OnBuild2;
                @Build2.canceled -= m_Wrapper.m_BuildingActionsCallbackInterface.OnBuild2;
                @Build3.started -= m_Wrapper.m_BuildingActionsCallbackInterface.OnBuild3;
                @Build3.performed -= m_Wrapper.m_BuildingActionsCallbackInterface.OnBuild3;
                @Build3.canceled -= m_Wrapper.m_BuildingActionsCallbackInterface.OnBuild3;
                @UndoSelection.started -= m_Wrapper.m_BuildingActionsCallbackInterface.OnUndoSelection;
                @UndoSelection.performed -= m_Wrapper.m_BuildingActionsCallbackInterface.OnUndoSelection;
                @UndoSelection.canceled -= m_Wrapper.m_BuildingActionsCallbackInterface.OnUndoSelection;
                @MousePosition.started -= m_Wrapper.m_BuildingActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_BuildingActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_BuildingActionsCallbackInterface.OnMousePosition;
                @Rotate.started -= m_Wrapper.m_BuildingActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_BuildingActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_BuildingActionsCallbackInterface.OnRotate;
            }
            m_Wrapper.m_BuildingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @RigthClick.started += instance.OnRigthClick;
                @RigthClick.performed += instance.OnRigthClick;
                @RigthClick.canceled += instance.OnRigthClick;
                @Build1.started += instance.OnBuild1;
                @Build1.performed += instance.OnBuild1;
                @Build1.canceled += instance.OnBuild1;
                @Build2.started += instance.OnBuild2;
                @Build2.performed += instance.OnBuild2;
                @Build2.canceled += instance.OnBuild2;
                @Build3.started += instance.OnBuild3;
                @Build3.performed += instance.OnBuild3;
                @Build3.canceled += instance.OnBuild3;
                @UndoSelection.started += instance.OnUndoSelection;
                @UndoSelection.performed += instance.OnUndoSelection;
                @UndoSelection.canceled += instance.OnUndoSelection;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
            }
        }
    }
    public BuildingActions @Building => new BuildingActions(this);

    // Test
    private readonly InputActionMap m_Test;
    private ITestActions m_TestActionsCallbackInterface;
    private readonly InputAction m_Test_SpawnEnemy;
    public struct TestActions
    {
        private @InputPlayer m_Wrapper;
        public TestActions(@InputPlayer wrapper) { m_Wrapper = wrapper; }
        public InputAction @SpawnEnemy => m_Wrapper.m_Test_SpawnEnemy;
        public InputActionMap Get() { return m_Wrapper.m_Test; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestActions set) { return set.Get(); }
        public void SetCallbacks(ITestActions instance)
        {
            if (m_Wrapper.m_TestActionsCallbackInterface != null)
            {
                @SpawnEnemy.started -= m_Wrapper.m_TestActionsCallbackInterface.OnSpawnEnemy;
                @SpawnEnemy.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnSpawnEnemy;
                @SpawnEnemy.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnSpawnEnemy;
            }
            m_Wrapper.m_TestActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SpawnEnemy.started += instance.OnSpawnEnemy;
                @SpawnEnemy.performed += instance.OnSpawnEnemy;
                @SpawnEnemy.canceled += instance.OnSpawnEnemy;
            }
        }
    }
    public TestActions @Test => new TestActions(this);
    public interface IPlayerMainActions
    {
        void OnMovement(InputAction.CallbackContext context);
    }
    public interface IBuildingActions
    {
        void OnLeftClick(InputAction.CallbackContext context);
        void OnRigthClick(InputAction.CallbackContext context);
        void OnBuild1(InputAction.CallbackContext context);
        void OnBuild2(InputAction.CallbackContext context);
        void OnBuild3(InputAction.CallbackContext context);
        void OnUndoSelection(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
    }
    public interface ITestActions
    {
        void OnSpawnEnemy(InputAction.CallbackContext context);
    }
}
