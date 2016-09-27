namespace EcoTile
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "TileRoadLookupTable", menuName = "Data/LookupTables/TileRoad", order = 2)]
    class TileRoadLookupTable : ScriptableObject
    {
        [SerializeField]
        GameObject[] tileRoadLookupTable;

        public GameObject getTile(TileRoadType type)
        {
            return tileRoadLookupTable[(int)type];
        }
    }
}
