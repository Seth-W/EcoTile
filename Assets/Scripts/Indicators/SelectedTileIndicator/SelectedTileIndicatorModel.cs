namespace EcoTile
{
    using UnityEngine;
    using System.Collections;
    using System;

    class SelectedTileIndicatorModel : ObjectModel 
    {
        [SerializeField]
        GameObject TileGlow;
        Renderer rend;

        void OnEnable()
        {
            rend = GetComponent<Renderer>();

            NodeManager.activeNodeUpdateEvent += OnActiveNodeUpdateEvent;
            NodeManager.nodeDeleteEvent += OnNodeDeleteEvent;
        }
        void OnDisable()
        {
            NodeManager.activeNodeUpdateEvent -= OnActiveNodeUpdateEvent;
            NodeManager.nodeDeleteEvent -= OnNodeDeleteEvent;
        }

        /**
        *<summary>
        *Responds to <see cref="NodeManager.activeNodeUpdateEvent"/>. 
        *If the passed node is not null enables the SelectedTileIndicator
        *otherwise disables the indicator
        *</summary>
        */
        void OnActiveNodeUpdateEvent(NodePosition nodePos)
        {
            if (NodeManager.getNode(nodePos) == null)
            {
                active = false;
            }
            //If there's a node in the array, active == true
            else
            {
                active = true;
            }
        }

        /**
        *<summary>
        *Responds to <see cref="NodeManager.nodeDeleteEvent"/>. 
        *Deactivates the active tile indicator
        *</summary>
        */
        void OnNodeDeleteEvent(NodePosition nodePos)
        {
            active = false;
        }

        /**
        *<summary>
        *Called by an ObjectModel Componenet's setActive(true method)
        *</summary>
        */
        public override void Activate()
        {
            //Debug.Log("Activate called for " + this);
            rend.enabled = true;
            TileGlow.SetActive(true);
        }

        /**
        *<summary>
        *Called by an ObjectModel Componenet's setActive(true method)
        *</summary>
        */
        public override void Deactivate()
        {
            //Debug.Log("Deactivate called for " + this);
            rend.enabled = false;
            TileGlow.SetActive(false);
        }

        protected override void Start()
        {
            base.Start();
            active = false;
        }

        void Update()
        {

        }

    }
}