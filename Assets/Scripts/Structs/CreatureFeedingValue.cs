namespace EcoTile
{
    using UnityEngine;
	using UnityEngine.UI;

    [System.Serializable]
    struct CreatureData
    {
		public Sprite sprite;
        public GameObject creaturePrefab;
        public int energyCostPerTick;
        public int energyCostPerSpawn;
        public bool[] feedingEnabled;
        public int[] percentOfResourceAvailable;
    }
}