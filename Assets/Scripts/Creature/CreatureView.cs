﻿namespace EcoTile
{
    using UnityEngine;

    class CreatureView : MonoBehaviour, IObjectView 
    {
        Renderer rend;

        void OnEnable()
        {
            rend = GetComponent<Renderer>();

            CameraView.cameraInZoomEvent += OnZoomBeginEvent;
            CameraView.CameraZoomFinishEvent += OnZoomEndEvent;
        }

        void OnDisable()
        {
            CameraView.cameraInZoomEvent -= OnZoomBeginEvent;
            CameraView.CameraZoomFinishEvent -= OnZoomEndEvent;
        }

        void OnZoomBeginEvent(bool zoomIn)
        {
            setOutlineThickness(0);
        }

        void OnZoomEndEvent(bool zoomIn)
        {
            setOutlineThickness(.001f);
            Debug.Log("Settomg outline thickness");
        }


        /**
        *<summary>
        *Called by and ObjectModel when the objectModel Enabled property is set to true
        *</summary>
        */
        public void OnActivate()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by and ObjectModel when the objectModel Enabled property is set to false
        *</summary>
        */
        public void OnDeactivate()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by an ObjectControl on the first frame that the mouse is no longer hovering over this gameObject when it previously had been
        *</summary>
        */
        public void OnHoverOff()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by an ObjectControl on the first frame that the mouse hovers over this gameObject
        *</summary>
        */
        public void OnHoverOn()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by and ObjectControl on the first frame that the mouse left clicks down while hovering over this gameObject
        *</summary>
        */
        public void OnPrimaryMouseDown()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called on the first frame for the mouseclicked object that
        *-- while the left mouse button is held down-- 
        *the mousepicked object does not equal the mouseclicked object
        *</summary>
        */
        public void OnPrimaryMouseDownRevert()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by and ObjectControl on the first frame that the mouse left clicks up after left clicking down on this object
        *</summary>
        */
        public void OnPrimaryMouseUp()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Sets the outline color to a given <see cref="Color"/>
        *</summary>
        */
        public void setOutlineColor(Color newColor)
        {
            rend.material.SetColor("_OutlineColor", newColor);
        }
        /**
        *<summary>
        *Sets the outline color to a given <see cref="Vector4"/>
        *</summary>
        */
        public void setOutlineColor(Vector4 newColor)
        {
            rend.material.SetColor("_OutlineColor", newColor);
        }

        /**
        *<summary>
        *Sets the outline width to a given <see cref="float"/>
        *</summary>
        */
        public void setOutlineThickness(float newThickness)
        {
            rend.material.SetFloat("_Outline", Mathf.Clamp(newThickness, 0, 10));
        }
    }
}