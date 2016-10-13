namespace EcoTile
{
    using UnityEngine;

    class Tick
    {
        int[,] nodeUpdateData;

        public Tick(int[,] nodeUpdateData)
        {
            this.nodeUpdateData = nodeUpdateData;
        }

        public int getNodeData(int xIndex, int zIndex)
        {
            return nodeUpdateData[xIndex, zIndex];
        }
    }
}
