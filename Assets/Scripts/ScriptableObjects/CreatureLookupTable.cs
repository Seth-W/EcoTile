namespace EcoTile
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "CreatureLookupTable", menuName = "Data/LookupTables/Creature", order = 1)]
    class CreatureLookupTable : ScriptableObject
    {
        [SerializeField, Range(0, 1)]
        float creature0, creature1, creature2, creature3, creature4, creature5,
            creature6, creature7, creature8, creature9;

    }
}
