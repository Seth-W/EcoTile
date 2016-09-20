namespace EcoTile
{
    using UnityEngine;

    public struct NodePosition
    {
        public Vector3 position;
        public int xIndex, zIndex;
        public bool inRange;

        public NodePosition(Vector3 pos)
        {
            Debug.Log("Position is: " + pos);

            xIndex = Mathf.FloorToInt(pos.x) + NodeManager.MapWidth / 2;
            zIndex = Mathf.FloorToInt(pos.z) + NodeManager.MapLength / 2;

            inRange = (xIndex > -1 && xIndex < NodeManager.MapWidth) && (zIndex > -1 && zIndex < NodeManager.MapLength);

            position.x = Mathf.Floor(pos.x) + 0.5f;
            position.y = -0.03f;
            position.z = Mathf.Floor(pos.z) + 0.5f;
            
            Debug.Log("Coordinates are: (" + xIndex + "," + zIndex + ")");
            Debug.Log("NodePosition is: " + position);
            Debug.Log(inRange.ToString());
        }
    }
}
