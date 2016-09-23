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

        public int getSliderValue()
        {
            return (int)sliderComponent.value;
        }


        protected override void Start()
        {
            base.Start();
        }

        void Update()
        {

        }

        void OnActiveNodeUpdate(NodePosition nodePos)
        {
            activeNode = nodePos;
        }
    }
}