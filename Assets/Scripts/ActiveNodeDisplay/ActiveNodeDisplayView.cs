namespace EcoTile
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    class ActiveNodeDisplayView : MonoBehaviour, IObjectView 
    {
        [SerializeField]
        Text activeNodeLabel;
        [SerializeField]
        Text[] activeNodeCreatureNumbers;
        

        void OnEnable()
        {
            NodeManager.activeNodeUpdateEvent += OnActiveNodeUpdateEvent;
            SlidersControl.SliderValueUpdateEvent += OnSliderValueUpdateEvent;
        }
        void OnDisable()
        {
            NodeManager.activeNodeUpdateEvent -= OnActiveNodeUpdateEvent;
            SlidersControl.SliderValueUpdateEvent -= OnSliderValueUpdateEvent;
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
        *Responds to <see cref="NodeManager"/> active node update events
        *Updates the ActiveNodeLabel
        *Updates the 
        *</summary>
        */
        void OnActiveNodeUpdateEvent(NodePosition newActiveNode)
        {
            updateActiveNodeLabel(newActiveNode);

            NodeModel activeNodeModel = NodeManager.getNode(newActiveNode);

            updateStatsLabel(activeNodeModel.creatureAmounts);
        }

        /**
        *<summary>
        *Responds to <see cref="SlidersControl"/> OnSliderValueUpdateEvent to update the stats label 
        *</summary>
        */
        private void OnSliderValueUpdateEvent(int index, int value)
        {
            updateStatsLabel(index, value);
        }

        /**
        *<summary>
        *Updates the stats label to reflect the active node's current creature amounts at a given index
        *</summary>
        */
        public void updateStatsLabel(int index, int newValue)
        {
            activeNodeCreatureNumbers[index].text = newValue.ToString();
        }

        /**
        *<summary>
        *Updates the stats label to reflect the active node's current creature amounts at a given index
        *</summary>
        */
        public void updateStatsLabel(int[] newValues)
        {
            Debug.Log(newValues[0]);
            for (int i = 0; i < 10; i++)
            {
                activeNodeCreatureNumbers[i].text = newValues[i].ToString();
            }
        }


        /**
        *<summary>
        *Updates the ActiveNode label to reflect the active node
        *</summary>
        */
        public void updateActiveNodeLabel(NodePosition newActiveNode)
        {
            activeNodeLabel.text = newActiveNode.ToString();
        }
    }
}