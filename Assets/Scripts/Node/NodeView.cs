namespace EcoTile
{
    using UnityEngine;
    using System.Collections;
    using System;

    class NodeView : MonoBehaviour, IObjectView
    {
        NodeModel model;

        [SerializeField]
        TileRoadLookupTable roadLookupTable;

        void Start()
        {

        }

        public void OnEnable()
        {
            //Debug.Log("The NodeView OnEnable was called");
            //Init the properties
            model = GetComponent<NodeModel>();

            //Subscribe to corresponding NodeModel events
            model.NodeModelCreatureAmountsUpdateEvent += OnNodeModelCreatureAmountsUpdate;
        }

        void OnDisable()
        {
            //Unsubscribe to corresponding NodeModel events
            model.NodeModelCreatureAmountsUpdateEvent -= OnNodeModelCreatureAmountsUpdate;
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
        *Called by NodeModel when the creature amounts array is updated
        *Updates the visual feedback of the node 
        *</summary>
        */
        private void OnNodeModelCreatureAmountsUpdate(int[] updatedAmounts)
        {
            Debug.LogWarning("The requested method is a stub");
        }
    }
}