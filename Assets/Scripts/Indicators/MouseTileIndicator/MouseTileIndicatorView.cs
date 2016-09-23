namespace EcoTile
{
    using UnityEngine;
    using System.Collections;
    using System;

    class MouseTileIndicatorView : MonoBehaviour, IObjectView 
    {
        [SerializeField, Range(0, 0.05f)]
        float indicatorHeight;


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
        *Sets the position of the attached GameObject to a given position on the XZ plane w/ an offset on the Y-Axis
        *The y-offset is given by the exposed inspector property <see cref="indicatorHeight"/>
        *</summary>
        */
        public void setIndicatorPosition(Vector3 newPos)
        {
            transform.position = new Vector3(newPos.x, indicatorHeight, newPos.z);
        }

        /**
        *<summary>
        *Sets the position of the attached GameObject from a given <see cref="NodePosition"/> on the XZ plane w/ an offset on the Y-Axis
        *The y-offset is given by the exposed inspector property <see cref="indicatorHeight"/>
        *</summary>
        */
        public void setIndicatorPosition(NodePosition nodePos)
        {
            transform.position = new Vector3(nodePos.position.x, indicatorHeight, nodePos.position.z);
        }
    }
}