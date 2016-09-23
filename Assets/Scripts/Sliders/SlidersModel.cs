namespace EcoTile
{
    using UnityEngine;
    using UnityEngine.UI;

    class SlidersModel : ObjectModel 
    {
        public NodePosition activeNode;
        Slider sliderComponent;

        public int statIndex;          //The stat this slider controls

        void OnEnable()
        {
            //Initial GetComponent Calls
            sliderComponent = GetComponent<Slider>();
            
            //Subscribe to NodeManager events
            NodeManager.activeNodeUpdateEvent += OnActiveNodeUpdate;
        }
        void OnDisable()
        {
            //Unsubscribe to NodeManager events
            NodeManager.activeNodeUpdateEvent -= OnActiveNodeUpdate;
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
        *Responds to <see cref="NodeManager"/> active node update events
        *Updates the active node <see cref="NodePosition"/>
        *</summary>
        */
        void OnActiveNodeUpdate(NodePosition nodePos)
        {
            activeNode = nodePos;
        }
    }
}