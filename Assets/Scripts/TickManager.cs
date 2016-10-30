namespace EcoTile
{
    using UnityEngine;
    using System.Collections.Generic;

    class TickManager : MonoBehaviour
    {
        public delegate void TickEvent(Tick newTick);
        public static TickEvent TickUpdateEvent;

        [SerializeField]
        float timePerTick;
        [SerializeField]
        bool tickAutomatically;

        float timeSinceLastTick;

        int[,,] creatureAmountsByTile_ForFeeding;

        void OnEnable()
        {
            creatureAmountsByTile_ForFeeding = new int[NodeManager.MapWidth, NodeManager.MapLength, DataManager.amountOfCreatures];

            //            InputManager.FrameInputEvent += OnFrameInput;
            timeSinceLastTick = 0f;
        }
        void OnDisable()
        {
//            InputManager.FrameInputEvent -= OnFrameInput;
        }

        void Update()
        {
            if(!tickAutomatically)
            {
                if(Input.GetKeyDown(KeyCode.Tab))
                {
                    TickUpdateEvent(getNextTick());
                }
            }
            
            else
            {
                timeSinceLastTick += Time.deltaTime;
                if(timeSinceLastTick >= timePerTick)
                {
                    TickUpdateEvent(getNextTick());
                    timeSinceLastTick = 0f;
                }
            }
        }

        /**
        *<summary>
        *Surveys the game state and returns a <see cref="Tick"/> to be sent in <see cref"TickUpdateEvent"/>
        *</summary>
        */
        Tick getNextTick()
        {
            NodeTickInputData[,] tickData = new NodeTickInputData[NodeManager.MapWidth, NodeManager.MapLength];
            Queue<NodePosition>[] tilesByCreatureTypeQueue = new Queue<NodePosition>[DataManager.amountOfCreatures];

            for (int i = 0; i < DataManager.amountOfCreatures; i++)
            {
                tilesByCreatureTypeQueue[i] = new Queue<NodePosition>();
            }

            for (int i = 0; i < NodeManager.MapWidth; i++)
            {
                for(int j = 0; j < NodeManager.MapLength; j++)
                {
                    NodeModel workingNode = NodeManager.getNode(i, j);
                    if(workingNode != null)
                    {
                        tickData[i,j] = workingNode.getNodeTickData();
                        tilesByCreatureTypeQueue[(int)workingNode.type].Enqueue(workingNode.nodePos);
                    }
                }
            }
            return new Tick(tickData, tilesByCreatureTypeQueue);
        }
        
        int[,,] getCurrentCreatureAmountsByTile()
        {
            int[,,] retValue = new int[NodeManager.MapWidth, NodeManager.MapLength, DataManager.amountOfCreatures];

            for (int i = 0; i < NodeManager.MapWidth; i++)
            {
                for (int j = 0; j < NodeManager.MapLength; j++)
                {
                    for (int k = 0; k < DataManager.amountOfCreatures; k++)
                    {
                        retValue[i, j, k] = NodeManager.getNode(i, j).getCreatureAmount(k);
                    }
                }
            }

            return retValue;
        }

        int[,] calculateSurplusValues(NodeTickInputData[,] tickInputData, Queue<NodePosition>[] nodePosQueue)
        {
            int[,,] currentCreatureAmountsByTile = getCurrentCreatureAmountsByTile();

            for (int i = 1; i < DataManager.amountOfCreatures - 1; i++)
            {
                while(nodePosQueue.Length > 0)
                {

                }
            }

            return new int[NodeManager.MapWidth, NodeManager.MapLength];
        }
    }
}
