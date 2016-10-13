namespace EcoTile
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "CreatureLookupTable", menuName = "Data/LookupTables/Creature", order = 1)]
    class CreatureLookupTable : ScriptableObject
    {
        [SerializeField]
        public CreatureFeedingValue[] table; 
    }
}
