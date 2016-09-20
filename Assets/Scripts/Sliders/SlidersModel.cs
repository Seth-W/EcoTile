namespace EcoTile
{
    using UnityEngine;
    using System.Collections;
    using System;

    class SlidersModel : ObjectModel 
    {
        public NodePosition activeNode;

        void OnEnable()
        {
            NodeManager.activeNodeUpdateEvent += OnActiveNodeUpdate;
        }
        void OnDisable()
        {
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