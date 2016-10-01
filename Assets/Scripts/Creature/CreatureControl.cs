namespace EcoTile
{
    using UnityEngine;
    using System.Collections;
    using System;

    class CreatureControl :  MonoBehaviour, IObjectControl
    {
        CreatureView view;
        CreatureModel model;


        void OnEnable()
        {
            view = GetComponent<CreatureView>();
            model = GetComponent<CreatureModel>();
        }
        void OnDisable()
        {

        }
        
        /**
        *<summary>
        *Called on the first frame that a ray cast from the mouse's position on the screen no longer collides with this object
        *</summary>
        */
        public void HoverOff()
        {
            //Debug.LogError("The requested method is not implemented");
            view.OnHoverOff();
            Debug.Log(this + " called hoverOff");
        }

        /**
        *<summary>
        *Called on the frame when a ray cast from the mouse's position on the screen first collides with this object
        *</summary>
        */
        public void HoverOn()
        {
            view.OnHoverOn();
            Debug.Log(this + " called hoverOn");
            //Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called on the first frame that the left mouse button is pressed while a ray cast from the
        *mouse's position on the screen collides with this object
        *</summary>
        */
        public void PrimaryMouseDown()
        {
            //Debug.LogError("The requested method is not implemented");
            view.OnPrimaryMouseDown();
            model.pickUp();
        }

        /**
        *<summary>
        *Called on the first frame for the mouseclicked object that
        *-- while the left mouse button is held down-- the mousepicked object does not equal the mouseclicked object
        *</summary>
        */
        public void PrimaryMouseDownRevert()
        {
            //Debug.LogError("The requested method is not implemented");
            view.OnPrimaryMouseDownRevert();
        }

        /**
        *<summary>
        *Called on the first frame that the left mouse button is released after MouseDown() has been called
        *</summary>
        */
        public void PriamryMouseUp()
        {
            //Debug.LogError("The requested method is not implemented");
            view.OnPrimaryMouseUp();
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
