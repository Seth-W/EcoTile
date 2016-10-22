namespace EcoTile
{
    using UnityEngine;
    using EcoTile.ExtensionMethods;
    using UnityEngine.UI;
    using UI;

    class InputManager : MonoBehaviour
    {
        public delegate void InputEvent(InputEventData inputData);
        public static InputEvent FrameInputEvent;

        bool zoomedInState;
        MousePicker picker;
        ToolBoxEnum toolType;


        void OnEnable()
        {
            picker = new MousePicker();

            CameraView.CameraZoomFinishEvent += OnCameraZoomFinish;
            PersistentToggleGroup.ToolUpdateEvent += OnToolUpdate;
			CreaturePopup.ToolUpdateEvent += OnToolUpdate;
        }

        void OnDisable()
        {
            CameraView.CameraZoomFinishEvent -= OnCameraZoomFinish;
            PersistentToggleGroup.ToolUpdateEvent -= OnToolUpdate;
			CreaturePopup.ToolUpdateEvent -= OnToolUpdate;
		}


        void Start()
        {

        }

        void Update()
        {
            FrameInputEvent(getFrameInputData());
            if(zoomedInState)
            {
                picker.mousePickObjectControl();
            }
        }

        InputEventData getFrameInputData()
        {
            return new InputEventData(toolType);
        }

        void OnCameraZoomFinish(bool zoomedIn)
        {
            zoomedInState = zoomedIn;
        }

        void OnToolUpdate(ToolBoxEnum type)
        {
            setToolType(type);
        }

        public void setToolType(ToolBoxEnum type)
        {
            if (toolType != type)
            {
                toolType = type;
            }
        }

    }
}