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

        public static int amountOfCreatures;

        static CreatureLookupTable _creatureLookupTable_Static;
        static TileRoadLookupTable _tileRoadLookupTable_Static;

        void Awake()
        {
            for (int i = 0; i < _creatureLookupTable.creatureData.Length; i++)
            {
                _creatureLookupTable.creatureData[i].percentOfResourceAvailable = new int[_creatureLookupTable.creatureData.Length];
            }


            if (_creatureLookupTable_Static == null)
            {
                _creatureLookupTable_Static = _creatureLookupTable;
            }
            else
            {
                Destroy(this);
            }

            if (_tileRoadLookupTable_Static == null)
            {
                _tileRoadLookupTable_Static = _tileRoadLookupTable;
            }
            else
            {
                Destroy(this);
            }

            amountOfCreatures = _creatureLookupTable.creatureData.Length;
        }

        void OnEnable()
        {
            SliderGroup.CreatureSliderUpdateEvent += OnCreatureSliderUpdateEvent;
        }

        void OnDisable()
        {
            SliderGroup.CreatureSliderUpdateEvent -= OnCreatureSliderUpdateEvent;
        }

        void OnCreatureSliderUpdateEvent(CreatureType buttonType, CreatureType sliderType, int value)
        {
            creatureLookupTable.creatureData[(int)buttonType].percentOfResourceAvailable[(int)sliderType] = value;
        }

    }
}