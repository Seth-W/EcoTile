namespace EcoTile
{
    using UnityEngine;
    using System.Collections;
    using System;

    class NodeModel : ObjectModel 
    {
        public int[] creatureAmounts;
        
        
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
            creatureAmounts = new int[10];
        }

        void Update()
        {

        }

        public void init(int[] initCreatureAmounts)
        {
            if(initCreatureAmounts.Length != 10)
            {
                Debug.LogError("The array passed to init is not confiugred properly");
                return;
            }else
            {
                creatureAmounts = initCreatureAmounts;
            }

        }

        public void updateCreatureAmount(int index, int n)
        {
            creatureAmounts[index] = n;
        }
    }
}