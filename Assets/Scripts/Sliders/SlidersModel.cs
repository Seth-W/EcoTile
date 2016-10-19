namespace EcoTile
{
    using UnityEngine;
    using UnityEngine.UI;

    class SlidersModel : ObjectModel 
    {
        public NodePosition activeNode;
        Slider sliderComponent;

        SlidersControl control;
        SlidersView view;

        public int statIndex;          //The stat this slider controls

        void OnEnable()
        {
            //Initial GetComponent Calls
            sliderComponent = GetComponent<Slider>();
            control = GetComponent<SlidersControl>();
            view = GetComponent<SlidersView>();
            
            //Subscribe to NodeManager events
            NodeManager.activeNodeUpdateEvent += OnActiveNodeUpdate;
            NodeManager.nodeDeleteEvent += OnNodeDelete;
            NodeModel.NodeModelRefreshActiveNodeEvent += OnRefreshActiveNodeEvent;

        }
        void OnDisable()
        {
            //Unsubscribe to NodeManager events
            NodeManager.activeNodeUpdateEvent -= OnActiveNodeUpdate;
            NodeManager.nodeDeleteEvent -= OnNodeDelete;
            NodeModel.NodeModelRefreshActiveNodeEvent -= OnRefreshActiveNodeEvent;
        }
        
        /**
        *<summary>
        *Called by an ObjectModel Componenet's setActive(true method)
        *</summary>
        */
        public override void Activate()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by an ObjectModel Componenet's setActive(true method)
        *</summary>
        */
        public override void Deactivate()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Returns the value for the attached slider as an integer (Truncates)
        *</summary>
        */
        public int getSliderValue()
        {
            return (int)sliderComponent.value;
        }

        /**
        *<summary>
        *Sets the value of the attached slider component to a given new value
        *</summary>
        */
        public void setSliderValue(int newValue)
        {
            sliderComponent.value = newValue;
        }

        protected override void Start()
        {
            base.Start();
        }

        void Update()
        {

        }


        /**
        *<summary>
        *Responds to <see cref="NodeManager.activeNodeUpdateEvent"/> 
        *Updates the active node <see cref="NodePosition"/>
        *</summary>
        */
        void OnActiveNodeUpdate(NodePosition nodePos)
        {
            activeNode = nodePos;
            control.controlEnabled = true;
            setSliderValue(NodeManager.getNode(nodePos).creatureAmounts[statIndex]);
        }

        /**
        *<summary>
        *Responds to <see cref="NodeManager.nodeDeleteEvent"/>
        *Updates the active node <see cref="NodePosition"/>
        *</summary>
        */
        void OnNodeDelete(NodePosition nodePos)
        {
            control.controlEnabled = false;
            setSliderValue(0);
        }

        /**
        *<summary>
        *Refreshes the slider value for the active node on a tick update event
        *</summary>
        */
        void OnRefreshActiveNodeEvent(int[] creatureAmounts)
        {
            //Debug.LogWarning("OnTickUpdateEvent called for SlidersModel");
            setSliderValue(creatureAmounts[statIndex]);
        }
    }
}