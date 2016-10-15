namespace EcoTile
{
    using UnityEngine;

    class TickManager : MonoBehaviour
    {
        public delegate void TickEvent(Tick newTick);
        public static TickEvent TickUpdateEvent;

        [SerializeField]
        float timePerTick;
        [SerializeField]
        bool tickAutomatically;

        float timeSinceLastTick;

        void OnEnable()
        {
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
            int[,] tickData = new int[NodeManager.MapWidth, NodeManager.MapLength];
            for (int i = 0; i < NodeManager.MapWidth; i++)
            {
                for(int j = 0; j < NodeManager.MapLength; j++)
                {
                    NodeModel workingNode = NodeManager.getNode(i, j);
                    if(workingNode != null)
                    {
                        tickData[i,j] = workingNode.queryNeighbors();
                    }
                }
            }
            return new Tick(tickData);
        } 
    }
}
