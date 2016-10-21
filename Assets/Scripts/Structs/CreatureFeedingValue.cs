namespace EcoTile
{
    using UnityEngine;
	using UnityEngine.UI;

    [System.Serializable]
    struct CreatureData
    {
		public Sprite sprite;
        public GameObject creaturePrefab;
        public bool[] feedingEnabled;
        public int[] amountsOfEachToFeed;
    }
}