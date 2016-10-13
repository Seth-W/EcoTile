namespace EcoTile
{
    using UnityEngine;
    using System.Collections;
    using System;
    using System.Collections.Generic;

    class NodeModel : ObjectModel 
    {
        public delegate void creatureAmountsUpdate(int[] updatedAmounts);

        public creatureAmountsUpdate NodeModelCreatureAmountsUpdateEvent;

        NodeControl control;
        NodeView view;

        [SerializeField]
        int[] _creatureAmounts;
        [SerializeField]
        int creatureType;

        [SerializeField]
        NodePosition nodePos;

        [SerializeField]
        bool _roadEnabled, _deletable;

        public bool roadEnabled
        {
            get { return _roadEnabled; }
        }
        public bool deletable
        {
            get { return _deletable; }
        }

        public int[] creatureAmounts
        {
            get { return _creatureAmounts.Clone() as int[]; }
        }

        bool visitedThisTickUpdatePass;

        [SerializeField]
        CreatureLookupTable table;

        void OnEnable()
        {
            control = GetComponent<NodeControl>();
            view = GetComponent<NodeView>();
        }

        void OnDisable()
        {

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
            //_creatureAmounts = new int[10];
        }

        void Update()
        {

        }

        /**
        *<summary>
        *Initializes the NodeModel's <see cref="NodePosition"/> field and <see cref="int[]"/> field to given parameters
        *</summary>
        */
        public void init(NodePosition nodePos, int[] initCreatureAmounts, bool deletable)
        {
            this.nodePos = nodePos;
            _deletable = deletable;
            visitedThisTickUpdatePass = false;

            if (initCreatureAmounts.Length != 10)
            {
                Debug.LogError("The array passed to init is not confiugred properly");
                return;
            }else
            {
                _creatureAmounts = initCreatureAmounts;
            }
        }


        /**
        *<summary>
        *Returns a creature amount at a given index
        *</summary>
        */
        public int getCreatureAmount(int index)
        {
            return _creatureAmounts[index];
        }

        /**
        *<summary>
        *Updates a the creatur amount at a given index
        *</summary>
        */
        public void updateCreatureAmount(int index, int n)
        {
            _creatureAmounts[index] = n;
            NodeModelCreatureAmountsUpdateEvent(_creatureAmounts.Clone() as int[]);
        }

        /**
        *<summary>
        *Takes an array of integers and updates the whole creature index
        *</summary>
        */
        public void updateCreatureAmount(int[] newAmounts)
        {
            _creatureAmounts = newAmounts;
            NodeModelCreatureAmountsUpdateEvent(_creatureAmounts.Clone() as int[]);
        }

        /**
        *<summary>
        *Deletes the game object this node is attached to
        *</summary>
        */
        public void deleteNode()
        {
            Destroy(gameObject);
        }

        /**
        *<summary>
        *Updates the roadEnabled field and redraws roads if the property changed
        *</summary>
        */
        public void setRoadEnabled(bool newValue)
        {
            //Early exit if no change to value
            if (_roadEnabled == newValue)
                return;
            _roadEnabled = newValue;
            redrawRoads();
        }

        /**
        *<summary>
        *Toggle the value of _roadEnabled and redrawRoads
        *</summary>
        */
        public void toggleRoadEnabled()
        {
            _roadEnabled = !_roadEnabled;
            redrawRoads();
        }

        /**
        *<summary>
        *Updates the road visuals for this node based on the neighbors
        *Calls 2nd gen redrawRoads for direct neighbors
        *</summary>
        */
        public void redrawRoads()
        {
            view.redrawRoads(nodePos);

            NodeModel temp = NodeManager.getNode(nodePos.xIndex + 1, nodePos.zIndex);

            if(temp != null)
                temp.redrawRoads2ndGen();

            temp = NodeManager.getNode(nodePos.xIndex - 1, nodePos.zIndex);
            if(temp != null)
                temp.redrawRoads2ndGen();

            temp = NodeManager.getNode(nodePos.xIndex, nodePos.zIndex + 1);
            if(temp != null)
                temp.redrawRoads2ndGen();

            temp = NodeManager.getNode(nodePos.xIndex, nodePos.zIndex - 1);
            if(temp != null)
                temp.redrawRoads2ndGen();
        }

        /**
        *<summary>
        *Updates the road visuals for this node based on the neighbors
        *2nd generation of redraw roads, doesn't have followup calls
        *</summary>
        */
        void redrawRoads2ndGen()
        {
            view.redrawRoads(nodePos);
        }

        public int queryNeighbors()
        {
            int[] retValue = new int[10];

            Stack<NodeModel> frontier = new Stack<NodeModel>();

            populateFrontier(frontier, this);

            NodeModel workingModel;
            int[] workingArray;

            while (frontier.Count > 0)
            {
                workingModel = frontier.Pop();
                workingArray = workingModel.creatureAmounts;
                workingModel.visitedThisTickUpdatePass = false;
                for(int i = 0; i < workingArray.Length; i++)
                {
                    retValue[i] += workingArray[i];
                }
            }
            CreatureFeedingValue feedingValues = table.table[creatureType];

            for(int i = 0; i < 10; i++ )
            {

            }
            return _creatureAmounts[0];            
        }

        void populateFrontier(Stack<NodeModel> stack, NodeModel node)
        {
            NodeModel workingModel = NodeManager.getNode(node.nodePos.xIndex + 1, node.nodePos.zIndex);
            if(workingModel != null)
            {
                stack.Push(workingModel);
                workingModel.visitedThisTickUpdatePass = true;
                addNeighbors(stack, workingModel);
            }

            workingModel = NodeManager.getNode(node.nodePos.xIndex - 1, node.nodePos.zIndex);
            if(workingModel != null)
            {
                stack.Push(workingModel);
                workingModel.visitedThisTickUpdatePass = true;
                addNeighbors(stack, workingModel);
            }

            workingModel = NodeManager.getNode(node.nodePos.xIndex, node.nodePos.zIndex + 1);
            if(workingModel != null)
            {
                stack.Push(workingModel);
                workingModel.visitedThisTickUpdatePass = true;
                addNeighbors(stack, workingModel);
            }

            workingModel = NodeManager.getNode(node.nodePos.xIndex, node.nodePos.zIndex - 1);
            if(workingModel != null)
            {
                stack.Push(workingModel);
                workingModel.visitedThisTickUpdatePass = true;
                addNeighbors(stack, workingModel);
            }
        }

        void addNeighbors(Stack<NodeModel> stack, NodeModel node)
        {
            if(node.roadEnabled)
            {
                NodeModel workingModel;
                
                workingModel = NodeManager.getNode(node.nodePos.xIndex + 1, node.nodePos.zIndex);
                if(workingModel != null)
                {
                    if(!workingModel.visitedThisTickUpdatePass)
                    {
                        stack.Push(workingModel);
                        workingModel.visitedThisTickUpdatePass = true;
                    
                        addNeighbors(stack, workingModel);
                    }
                }

                workingModel = NodeManager.getNode(node.nodePos.xIndex - 1, node.nodePos.zIndex);
                if(workingModel != null)
                {
                    if(!workingModel.visitedThisTickUpdatePass)
                    {
                        stack.Push(workingModel);
                        workingModel.visitedThisTickUpdatePass = true;
                    
                        addNeighbors(stack, workingModel);
                    }
                }    
                
                workingModel = NodeManager.getNode(node.nodePos.xIndex, node.nodePos.zIndex - 1);
                if(workingModel != null)
                {
                    if(!workingModel.visitedThisTickUpdatePass)
                    {
                        stack.Push(workingModel);
                        workingModel.visitedThisTickUpdatePass = true;
                    
                        addNeighbors(stack, workingModel);
                    }
                }

                workingModel = NodeManager.getNode(node.nodePos.xIndex, node.nodePos.zIndex + 1);
                if(workingModel != null)
                {
                    if(!workingModel.visitedThisTickUpdatePass)
                    {
                        stack.Push(workingModel);
                        workingModel.visitedThisTickUpdatePass = true;
                    
                        addNeighbors(stack, workingModel);
                    }
                }
            }
        }

    }
}