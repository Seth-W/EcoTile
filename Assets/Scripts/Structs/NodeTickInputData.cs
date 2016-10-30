namespace EcoTile
{
    using System.Collections.Generic;

    struct NodeTickInputData
    {
        public CreatureType type;
        public int amountOfCreaturesOnTile;

        public int[,,] foodSources;
        public Stack<NodePosition> neighborStack;

        public NodeTickInputData(CreatureType type, int creatureAmount, int[,,] foodSources, Stack<NodePosition> neighborStack)
        {
            this.type = type;
            amountOfCreaturesOnTile = creatureAmount;
            this.foodSources = foodSources;
            this.neighborStack = neighborStack;
        }
    }
}
