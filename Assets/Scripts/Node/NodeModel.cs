namespace EcoTile
{
    using UnityEngine;
    using System.Collections;
    using System;

    class NodeModel : ObjectModel 
    {
        public delegate void creatureAmountsUpdate(int[] updatedAmounts);

        public creatureAmountsUpdate NodeModelCreatureAmountsUpdateEvent;


        [SerializeField]
        int[] _creatureAmounts;

        public int[] creatureAmounts
        {
            get { return _creatureAmounts.Clone() as int[]; }
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
            _creatureAmounts = new int[10];
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
                _creatureAmounts = initCreatureAmounts;
            }

        }

        public void updateCreatureAmount(int index, int n)
        {
            _creatureAmounts[index] = n;
            NodeModelCreatureAmountsUpdateEvent(_creatureAmounts.Clone() as int[]);
        }

        public void updateCreaturAmount(int[] newAmounts)
        {
            _creatureAmounts = newAmounts;
            NodeModelCreatureAmountsUpdateEvent(_creatureAmounts.Clone() as int[]);
        }
    }
}