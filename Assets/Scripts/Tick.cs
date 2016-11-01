namespace EcoTile
{
    using UnityEngine;
    using System.Collections.Generic;

    class Tick
    {
        int[,] tickData;
        int[,] pollutionData;



        public Tick(NodeTickInputData[,] nodeUpdateData, Queue<NodePosition>[] tilesByCreatureQueue)
        {
            tickData = calculateSurplusValues(nodeUpdateData, tilesByCreatureQueue);
            pollutionData = calculatePollutionValue(tickData);
        }
        
        /**
        *<summary>
        *Returns the integer representing deficit surplus for a tile at a given index
        *A positive number for surplus, a negative for a deficit, a 0 for neither
        *</summary>
        */
        public int getNodeData(int xIndex, int zIndex)
        {
            return tickData[xIndex, zIndex];
        }
        
        /**
        *<summary>
        *Returns the integer representing deficit surplus for a tile at a given <see cref="NodePositions"/>
        *A positive number for surplus, a negative for a deficit, a 0 for neither
        *</summary>
        */
        public int getNodeData(NodePosition nodePos)
        {
            return tickData[nodePos.xIndex, nodePos.zIndex];
        }

        /**
        *<summary>
        *Returns an integer representing how many characters will go unfed and 'die' this tick for a given <see cref="NodePosition"/>
        *</summary>
        */
        public int getNodePollutionData(NodePosition nodePos)
        {
            return pollutionData[nodePos.xIndex, nodePos.zIndex];
        }

        /**
        *<summary>
        *Returns an integer representing how many characters will go unfed and 'die' this tick for a given index
        *</summary>
        */
        public int getNodePollutionData(int xIndex, int zIndex)
        {
            return pollutionData[xIndex, zIndex];
        }

        /**
        *<summary>
        *Retuns a 2D array of integers representing whether a tile had a surplus or deficit of food for this tick
        *</summary>
        */
        int[,] calculateSurplusValues(NodeTickInputData[,] tickInputData, Queue<NodePosition>[] nodePosQueue)
        {
            int[,] retValue = new int[NodeManager.MapWidth, NodeManager.MapLength];

            for (int i = 1; i < DataManager.amountOfCreatures - 1; i++)
            {
                NodePosition workingNode;
                while (nodePosQueue[i].Count > 0)
                {
                    workingNode = nodePosQueue[i].Dequeue();
                    retValue[workingNode.xIndex, workingNode.zIndex] = calculateTileSurplus(getCurrentCreatureAmountsByTile(), tickInputData[workingNode.xIndex, workingNode.zIndex]);
                }
            }
            return retValue;
        }

        /**
        *<summary>
        *Returns a 3D array of integers capturing the current amount of resources (read: creatures) available for tiles to feed off of this tick
        *</summary>
        */
        int[,,] getCurrentCreatureAmountsByTile()
        {
            int[,,] retValue = new int[NodeManager.MapWidth, NodeManager.MapLength, DataManager.amountOfCreatures];

            NodeModel workingModel;

            for (int i = 0; i < NodeManager.MapWidth; i++)
            {
                for (int j = 0; j < NodeManager.MapLength; j++)
                {
                    for (int k = 0; k < DataManager.amountOfCreatures; k++)
                    {
                        workingModel = NodeManager.getNode(i, j);
                        if(workingModel != null)
                            retValue[i, j, k] = workingModel.getCreatureAmount(k);
                        
                        //Debug.Log(i + "," + j + "\n" + retValue[i, j, k]);
                    }
                }
            }

            return retValue;
        }

        /**
        *<summary>
        *Returns an int representing the surplus/deficit for a given Tile based on its <see cref="NodeTickInputData"/> and the current resources available for feeding
        *</summary>
        */
        int calculateTileSurplus(int[,,] creatureAmountsByTile, NodeTickInputData currentTileInputData)
        {
            int retValue = 0;
            
            int[] amountNeeded = getAmountOfResourcesNeeded(currentTileInputData);
            int[] surplus = new int[DataManager.amountOfCreatures];

            int deficit = DataManager.creatureLookupTable.creatureData[(int)currentTileInputData.type].energyCostPerTick * currentTileInputData.amountOfCreaturesOnTile;


            NodePosition nodePos;

            //Loop through the tiles stack of neighbors until fed
            while (currentTileInputData.neighborStack.Count > 0 || !checkIfFed(amountNeeded))
            {
                nodePos = currentTileInputData.neighborStack.Pop();
                
                for (int i = 0; i < DataManager.amountOfCreatures; i++)
                {
                    if (DataManager.creatureLookupTable.creatureData[(int)currentTileInputData.type].feedingEnabled[i])
                    {
                        int foodOfTypeOnTile = creatureAmountsByTile[nodePos.xIndex, nodePos.zIndex, i];
                        //Debug.Log("Food of type on tile: "+ foodOfTypeOnTile);

                        //If there is enough food on the tile to satisfy the remaining needs, set needs to 0 rather than a negative
                        if (foodOfTypeOnTile >= amountNeeded[i])
                        {
                            creatureAmountsByTile[nodePos.xIndex, nodePos.zIndex, i] -= amountNeeded[i];
                            surplus[i] += foodOfTypeOnTile - amountNeeded[i];
                            amountNeeded[i] = 0;
                            deficit -= foodOfTypeOnTile - amountNeeded[i];
                        }
                        else
                        {
                            creatureAmountsByTile[nodePos.xIndex, nodePos.zIndex, i] = 0;
                            amountNeeded[i] -= foodOfTypeOnTile;
                            deficit -= foodOfTypeOnTile;
                        }
                    }
                }
            }

            //Tally the total surplus/Deficit numbers
            for (int i = 0; i < DataManager.amountOfCreatures; i++)
            {
                retValue += surplus[i];
                retValue += amountNeeded[i];
                //Debug.Log("Surplus: " + surplus[i]);
                //Debug.Log("Deficit: " + amountNeeded[i]);
            }
            //Debug.Log(retValue);
            return -deficit;
        }

        /**
        *<summary>
        *Returns an array of integers representing the amount of each type of creature the given tile will need to be fully satisfied
        *</summary>
        */
        int[] getAmountOfResourcesNeeded(NodeTickInputData currentTileInputData)
        {
            int[] retValue = new int[DataManager.amountOfCreatures];
            DataManager.creatureLookupTable.creatureData[(int)currentTileInputData.type].amountsOfEachToFeed.CopyTo(retValue, 0);

            for (int i = 0; i < DataManager.amountOfCreatures; i++)
            {
                if (DataManager.creatureLookupTable.creatureData[(int)currentTileInputData.type].feedingEnabled[i])
                {
                    retValue[i] *= currentTileInputData.amountOfCreaturesOnTile;
                }
                else
                {
                    retValue[i] = 0;
                }
            }
            return retValue;
        }

        /**
        *<summary>
        *Returns true when all values in the given array are less than or equal to zero
        *</summary>
        */
        bool checkIfFed(int[] amountStillNeeded)
        {
            bool[] amountSatisfied = new bool[DataManager.amountOfCreatures];
            bool retValue = true;
            amountSatisfied[0] = amountStillNeeded[0] <= 0;
            for (int i = 1; i < amountStillNeeded.Length; i++)
            {
                amountSatisfied[i] = amountStillNeeded[i] >= 0;
                retValue = retValue && amountSatisfied[i - 1];
            }
            return retValue;
        }

        /**
        *<summary>
        *Returns an integer representing the total amount of pollution (Before a coefficient is applied) to be gained this tick
        *Calculates the number of creatures above the sustainable amount the population is on the tile and returns that number minus the number
        *of decomposers on the tile
        *</summary>
        */
        int[,] calculatePollutionValue(int[,] tickData)
        {
            int[,] retValue = new int[NodeManager.MapWidth, NodeManager.MapLength];

            for (int i = 0; i < NodeManager.MapWidth; i++)
            {
                for (int j = 0; j < NodeManager.MapLength; j++)
                {
                    retValue[i,j] = calculateTilePollutionValue(i, j);
                }
            }

            return retValue;
        }

        /**
        *<summary>
        *Returns Difference between the actual population on the tile and the amount of creatures the pop can support minus the number of decomposers on the tile
        *</summary>
        */
        int calculateTilePollutionValue(int xIndex, int zIndex)
        {
            NodeModel workingNode = NodeManager.getNode(xIndex, zIndex);
            if (workingNode == null)
                return 0;

            if(workingNode.type == CreatureType.VEGETATION)
            {
                return -workingNode.getCreatureAmount(DataManager.amountOfCreatures - 1);
            }
            int energyCostPerCreature = DataManager.creatureLookupTable.creatureData[(int)workingNode.type].energyCostPerTick;

            return tickData[xIndex, zIndex] / energyCostPerCreature - workingNode.getCreatureAmount(DataManager.amountOfCreatures - 1);
        }
    }
}
