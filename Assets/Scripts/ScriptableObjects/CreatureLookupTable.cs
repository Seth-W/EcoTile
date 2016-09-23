namespace EcoTile
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "CreatureLookupTable", menuName = "Data/LookupTables/Creature", order = 1)]
    class CreatureLookupTable : ScriptableObject
    {
        [SerializeField, Range(0, 1)]
        float[] creature1, creature2;

    }
}
