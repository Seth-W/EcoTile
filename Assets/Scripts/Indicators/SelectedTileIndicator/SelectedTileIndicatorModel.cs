namespace EcoTile
{
    using UnityEngine;
    using System.Collections;
    using System;

    class SelectedTileIndicatorModel : ObjectModel 
    {
        [SerializeField]
        GameObject TileGlow;

        void OnEnable()
        {
            NodeManager.activeNodeUpdateEvent += OnActiveNodeUpdateEvent;
        }
        void OnDisable()
        {
            NodeManager.activeNodeUpdateEvent -= OnActiveNodeUpdateEvent;
        }

        /**
        *<summary>
        *Responds to <see cref="NodeManager.activeNodeUpdateEvent"/>. 
        *If this is the first time an active node update event is called, enables the SelectedTileIndicator
        *and unsubscribes from <see cref="NodeManager.activeNodeUpdateEvent"/>
        *</summary>
        */
        void OnActiveNodeUpdateEvent(NodePosition nodePos)
        {
            Debug.Log("Enabling the Selected Tile Idicator with " + this);
            active = true;
            NodeManager.activeNodeUpdateEvent -= OnActiveNodeUpdateEvent;
        }

        /**
        *<summary>
        *Called by an ObjectModel Componenet's setActive(true method)
        *</summary>
        */
        public override void Activate()
        {
            Debug.Log("Activate called for " + this);
            GetComponent<Renderer>().enabled = true;
            TileGlow.SetActive(true);
        }

        /**
        *<summary>
        *Called by an ObjectModel Componenet's setActive(true method)
        *</summary>
        */
        public override void Deactivate()
        {
            Debug.Log("Deactivate called for " + this);
            GetComponent<Renderer>().enabled = false;
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