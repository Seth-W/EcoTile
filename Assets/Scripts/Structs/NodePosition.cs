namespace EcoTile
{
    using UnityEngine;

    [System.Serializable]
    public struct NodePosition
    {
        public Vector3 position;
        public int xIndex, zIndex;
        public bool inRange;

        public NodePosition(Vector3 pos)
        {
            //Debug.Log("Position is: " + pos);

            int xFloor = Mathf.FloorToInt(pos.x);
            int zFloor = Mathf.FloorToInt(pos.z);

            xIndex = xFloor % 2 == 0 ? xFloor : xFloor - 1;
            zIndex = zFloor % 2 == 0 ? zFloor : zFloor - 1;

            xIndex /= 2;
            zIndex /= 2;

            xIndex += NodeManager.MapWidth / 2;
            zIndex += NodeManager.MapLength / 2;

            inRange = (xIndex > -1 && xIndex < NodeManager.MapWidth) && (zIndex > -1 && zIndex < NodeManager.MapLength);

            position.x = xFloor % 2 == 0 ? xFloor + 1 : xFloor;
            position.y = -0.62f;
            position.z = zFloor % 2 == 0 ? zFloor  + 1: zFloor;

            //Debug.Log("Coordinates are: (" + xIndex + "," + zIndex + ")");
            //Debug.Log("NodePosition is: " + position);
            //Debug.Log(inRange.ToString());
        }

        public NodePosition(int xInt, int zInt)
        {
            xIndex = xInt;
            zIndex = zInt;

            inRange = (xIndex > -1 && xIndex < NodeManager.MapWidth) && (zIndex > -1 && zIndex < NodeManager.MapLength);

            position.x = 2 * xIndex - (NodeManager.MapWidth - 1);
            position.z = 2 * zIndex - (NodeManager.MapLength - 1);
            position.y = -0.62f;
        }

        public override string ToString()
        {
            return "Node " + xIndex + "," + zIndex;
        }
    }
}
