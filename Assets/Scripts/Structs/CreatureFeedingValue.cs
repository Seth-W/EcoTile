namespace EcoTile
{
    using UnityEngine;

    [System.Serializable]
    struct CreatureData
    {
        public GameObject creaturePrefab;
        public bool[] feedingEnabled;
        public int[] amountsOfEachToFeed;
    }
}