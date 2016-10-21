namespace EcoTile
{
    using UnityEngine;

    class DataManager : MonoBehaviour
    {
        [SerializeField]
        CreatureLookupTable _creatureLookupTable;
        [SerializeField]
        TileRoadLookupTable _tileRoadLookupTable;

        public static CreatureLookupTable creatureLookupTable
        { get {return _creatureLookupTable_Static; } }
        public static TileRoadLookupTable tileRoadLookupTable
        { get { return _tileRoadLookupTable_Static; } }

        static CreatureLookupTable _creatureLookupTable_Static;
        static TileRoadLookupTable _tileRoadLookupTable_Static;

        void Awake()
        {
            _creatureLookupTable_Static = _creatureLookupTable;
            _tileRoadLookupTable_Static = _tileRoadLookupTable;
        }

    }
}