namespace EcoTile
{
    using UnityEngine;
    using EcoTile.ExtensionMethods;
    using UnityEngine.UI;

    class InputManager : MonoBehaviour
    {
        public delegate void InputEvent(InputEventData inputData);
        public static InputEvent FrameInputEvent;


        void Start()
        {

        }

        void Update()
        {
            FrameInputEvent(getFrameInputData());
        }

        InputEventData getFrameInputData()
        {
            return new InputEventData("This doesn't do anything");
        }
    }
}