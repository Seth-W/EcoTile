namespace EcoTile
{
    using UnityEngine;
    using EcoTile.ExtensionMethods;
    using UnityEngine.UI;

    class InputManager : MonoBehaviour
    {
        public delegate void InputEvent(InputEventData inputData);
        public static InputEvent FrameInputEvent;

        bool zoomedInState;
        MousePicker picker;


        void OnEnable()
        {
            CameraView.CameraZoomFinishEvent += OnCameraZoomFinish;
            picker = new MousePicker();
        }

        void OnDisable()
        {
            CameraView.CameraZoomFinishEvent -= OnCameraZoomFinish;
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
            return new InputEventData("This doesn't do anything");
        }

        void OnCameraZoomFinish(bool zoomedIn)
        {
            zoomedInState = zoomedIn;
        }
    }
}