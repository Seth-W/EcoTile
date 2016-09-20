namespace EcoTile
{
    using UnityEngine;
    using System.Collections;
    using System;

    class CreatureModel : ObjectModel 
    {
        CreatureType _type;
        public CreatureType type
        {
            get { return _type; }
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
    }
}