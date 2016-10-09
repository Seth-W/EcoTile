namespace EcoTile
{
    using UnityEngine;

    class TickManager : MonoBehaviour
    {
        public delegate void TickEvent(Tick newTick);
        public static TickEvent TickUpdateEvent;

        Tick getNextTick()
        {
            return default(Tick);
        } 
    }
}
