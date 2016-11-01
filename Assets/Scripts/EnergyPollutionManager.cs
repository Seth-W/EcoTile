namespace EcoTile
{
    using UnityEngine;

    class EnergyPollutionManager : MonoBehaviour
    {
        public delegate void EnergyUpdate(int newEnergyValue);
        public delegate void PollutionUpdate(int newPollutionValue);

        public static EnergyUpdate EnergyUpdateEvent;
        public static PollutionUpdate PollutionUpdateEvent;

        [SerializeField]
        int _energyValue;
        int _pollutionValue;
        [SerializeField]
        int pollutionPerCreatureDeathPerTick;

        public static int energyValue;
        public static int pollutionValue;

        int[] population;
        [SerializeField]
        int[] energyPerCreaturePerTick;

        void OnEnable()
        {
            setEnergyValue(_energyValue);
            setPollutionValue(0);

            population = new int[DataManager.amountOfCreatures];

            NodeModel.CreaturePopulationIncrementEvent += OnCreaturePopulationIncrementEvent;
            TickManager.TickUpdateEvent += OnTickUpdateEvent;
            NodeManager.nodeCreateEvent += OnNodeCreateEvent;
            NodeManager.roadToggleEvent += OnRoadToggleEvent;
            NodeManager.creatureCreateEvent += OnCreatureCreateEvent;
        }
        
        void OnDisable()
        {
            NodeModel.CreaturePopulationIncrementEvent -= OnCreaturePopulationIncrementEvent;
            TickManager.TickUpdateEvent -= OnTickUpdateEvent;
            NodeManager.nodeCreateEvent -= OnNodeCreateEvent;
            NodeManager.roadToggleEvent -= OnRoadToggleEvent;
            NodeManager.creatureCreateEvent -= OnCreatureCreateEvent;
        }

        void OnCreatureCreateEvent(CreatureType type, int cost)
        {
            incrementEnergyValue(-cost);
        }

        void OnRoadToggleEvent(bool roadEnabled, int roadCost)
        {
            incrementEnergyValue(roadEnabled ? -roadCost : roadCost / 2);
        }

        void OnNodeCreateEvent(NodePosition nodePos, int nodeCost)
        {
            incrementEnergyValue(-nodeCost);
        }

        void OnCreaturePopulationIncrementEvent(CreatureType type, int incrementValue)
        {
            if(incrementValue < 0)
            {
                incrementPollutionValue(-incrementValue * pollutionPerCreatureDeathPerTick);
            }
            population[(int)type] += incrementValue;
            if (population[(int)type] < 0)
                population[(int)type] = 0; 
        }

        void OnTickUpdateEvent(Tick updateData)
        {
            int energyUpdateValue = 0;
            int pollutionUpdateValue = 0;

            for (int i = 1; i < DataManager.amountOfCreatures - 1; i++)
            {
                energyUpdateValue += energyPerCreaturePerTick[i] * population[i];
            }
            pollutionUpdateValue = population[DataManager.amountOfCreatures - 1] * energyPerCreaturePerTick[DataManager.amountOfCreatures - 1];

            incrementEnergyValue(energyUpdateValue + 1);
            incrementPollutionValue(pollutionUpdateValue);
        }

        /**
        *<summary>
        *
        *</summary>
        */
        public void setEnergyValue(int newVal)
        {
            _energyValue = newVal;
            energyValue = _energyValue;
            EnergyUpdateEvent(_energyValue);
        }

        /**
        *<summary>
        *
        *</summary>
        */
        public void incrementEnergyValue(int incrementValue)
        {
            _energyValue += incrementValue;
            energyValue = _energyValue;
            EnergyUpdateEvent(_energyValue);
        }

        /**
        *<summary>
        *
        *</summary>
        */
        public void setPollutionValue(int newVal)
        {
            _pollutionValue = newVal;
            pollutionValue = _pollutionValue;
            PollutionUpdateEvent(_pollutionValue);
        }

        /**
        *<summary>
        *
        *</summary>
        */
        public void incrementPollutionValue(int incrementValue)
        {
            _pollutionValue += incrementValue;
            pollutionValue = _pollutionValue;
            PollutionUpdateEvent(_pollutionValue);
        }
    }
}
