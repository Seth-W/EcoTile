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
        
        /**
        *<summary>
        *Returns the integer representing deficit surplus for a tile at a given index
        *A positive number for surplus, a negative for a deficit, a 0 for neither
        *</summary>
        */
        public int getNodeData(int xIndex, int zIndex)
        {
            return nodeUpdateData[xIndex, zIndex];
        }
        
        /**
        *<summary>
        *Returns the integer representing deficit surplus for a tile at a given <see cref="NodePositions"/>
        *A positive number for surplus, a negative for a deficit, a 0 for neither
        *</summary>
        */
        public int getNodeData(NodePosition nodePos)
        {
            return nodeUpdateData[nodePos.xIndex, nodePos.zIndex];
        }
    }
}
