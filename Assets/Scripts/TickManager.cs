namespace EcoTile
{
    using UnityEngine;

    class TickManager : MonoBehaviour
    {
        public delegate void TickEvent(Tick newTick);
        public static TickEvent TickUpdateEvent;


        void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                if(Input.GetKey(KeyCode.K))
                {
                    getNextTick();
                }
            }
        }

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

                    }
                }
            }
            return default(Tick);
        } 
    }
}
