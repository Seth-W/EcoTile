namespace EcoTile
{
    using UnityEngine;
    using UnityEngine.UI;
    using System;

    class SlidersControl : MonoBehaviour, IObjectControl
    {
        public delegate void SliderValueUpdate(int index, int newValue);

        public static SliderValueUpdate SliderValueUpdateEvent;

        SlidersModel model;
        SlidersView view;

        public bool controlEnabled;

        void OnEnable()
        {
            model = GetComponent<SlidersModel>();
            view = GetComponent<SlidersView>();
        }
        void OnDisable()
        {

        }

        void Update()
        {

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


        /**
        *<summary>
        *Called by the attached <see cref="Slider"/> component when the Slider Value Changes
        *</summary>
        */
        public void OnValueChanged()
        {
            if (controlEnabled)
            {
                SliderValueUpdateEvent(model.statIndex, model.getSliderValue());
            }
        }
    }
}
