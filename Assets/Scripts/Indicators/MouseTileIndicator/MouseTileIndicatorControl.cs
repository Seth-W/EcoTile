namespace EcoTile
{
    using UnityEngine;
    using System.Collections;
    using System;

    class MouseTileIndicatorControl :  MonoBehaviour, IObjectControl
    {
        MouseTileIndicatorView view;


        void OnEnable()
        {
            view = GetComponent<MouseTileIndicatorView>();

            InputManager.FrameInputEvent += OnFrameInputEvent;
        }
        void OnDisable()
        {
            InputManager.FrameInputEvent -= OnFrameInputEvent;
        }

        /**
        *<summary>
        *Responds to <see cref="InputManager.FrameInputEvent"/>. Called once per frame
        *Updates the position of the game object based on frame input data
        *</summary>
        */
        void OnFrameInputEvent(InputEventData data)
        {
            view.setIndicatorPosition(data.mouseXZProjectionStepwise);
        }


        /**
        *<summary>
        *Called on the first frame that a ray cast from the mouse's position on the screen no longer collides with this object
        *</summary>
        */
        public void HoverOff()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called on the frame when a ray cast from the mouse's position on the screen first collides with this object
        *</summary>
        */
        public void HoverOn()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called on the first frame that the left mouse button is pressed while a ray cast from the
        *mouse's position on the screen collides with this object
        *</summary>
        */
        public void PrimaryMouseDown()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called on the first frame for the mouseclicked object that
        *-- while the left mouse button is held down-- the mousepicked object does not equal the mouseclicked object
        *</summary>
        */
        public void PrimaryMouseDownRevert()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called on the first frame that the left mouse button is released after MouseDown() has been called
        *</summary>
        */
        public void PriamryMouseUp()
        {
            Debug.LogError("The requested method is not implemented");
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
