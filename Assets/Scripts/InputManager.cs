namespace EcoTile
{
    using UnityEngine;
    using EcoTile.ExtensionMethods;

    class InputManager : MonoBehaviour
    {
        public delegate void InputRelease(Vector3 fireWorldPosition);
        public static InputRelease InputReleaseEvent;


        void Start()
        {

        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                InputReleaseEvent(Camera.main.MousePickToXZPlane());
            }
        }
    }
}