﻿namespace EcoTile
{
    using UnityEngine;
    using EcoTile.UI;

    class NodeManager : MonoBehaviour
    {
        public delegate void activeNodeUpdate(NodePosition nodePos);
        public delegate void nodeDelete(NodePosition nodePos);
        public delegate void nodeCreate(NodePosition nodePos, int energyCost);
        public delegate void roadToggle(bool roadEnabled, int roadCost);
        public delegate void creatureCreate(int creatureCost);

        public static activeNodeUpdate activeNodeUpdateEvent;
        public static nodeDelete nodeDeleteEvent;
        public static nodeCreate nodeCreateEvent;
        public static roadToggle roadToggleEvent;
        public static creatureCreate creatureCreateEvent;
        
        [SerializeField]
        int _mapWidth, _mapLength;
        public static int MapWidth, MapLength;

        public static NodeManager nodeMaster;
        public static NodePosition activeNode;

        [SerializeField]
        VegetationDensity vegDensity;

        [SerializeField]
        int energyCostPerTile, energyCostPerRoad;

        [SerializeField]
        GameObject nodePrefab;
        [SerializeField]
        MessageButton messageInterface;

        public NodeModel[,] nodes;

        public bool genMap;


        void OnEnable()
        {
            InputManager.FrameInputEvent += OnInputEvent;
            SlidersControl.SliderValueUpdateEvent += OnSliderValueUpdateEvent;
            CreatureButton.CreatureButtonClickEvent += OnCreatureButtonClickEvent;
        }
        void OnDisable()
        {
            InputManager.FrameInputEvent -= OnInputEvent;
            SlidersControl.SliderValueUpdateEvent -= OnSliderValueUpdateEvent;
            CreatureButton.CreatureButtonClickEvent -= OnCreatureButtonClickEvent;
        }


        void Start()
        {
            init();
        }

        /**
        *<summary>
        *Handles the frame's <see cref="InputEventData"/>
        *Called once per frame
        *</summary>
        */
        void OnInputEvent(InputEventData inputData)
        {
            NodePosition nodePos = inputData.nodePos;
            if(!nodePos.inRange)
            {
                return;
            }

            //On left clicks spawns a node
            if (inputData.mouseInput.mouse0)
            {
                //Early dev measure to Stopgap not having a Fire State Machine
                if (inputData.toolType == ToolBoxEnum.CREATE)
                {
                    Debug.LogWarning("This should be removed in place of a Fire State machine");
                    spawnNode(nodePos);
                }

                //Update the activeNode
                if (inputData.toolType == ToolBoxEnum.SELECT)
                {
                    //If there is a node at the given position update the active node
                    if (getNode(nodePos) != null)
                    {
                        activeNode = nodePos;
                        activeNodeUpdateEvent(activeNode);
                    }
                }
            }

            //Toggle the road condition on the active node
            if(inputData.mouseInput.mouse0down)
            {
                if(inputData.toolType == ToolBoxEnum.ADD_ROAD)
                {
                    ToggleRoad(getNode(nodePos));
                }
            }

            //Delete the selected node
            if(inputData.mouseInput.mouse0down && inputData.toolType == ToolBoxEnum.DELETE)
            {
                deleteNode(inputData.nodePos);
            }
        }

        /**
        *<summary>
        *Responds To User Input on UI sliders
        *Changes the values on the ActiveNode
        *</summary>
        */
        void OnSliderValueUpdateEvent(int index, int value)
        {
            NodeModel activeNodeModel = getNode(activeNode);
            activeNodeModel.updateCreatureAmount(index, value);
        }

        void OnCreatureButtonClickEvent(CreatureType typeSelected)
        {
            if (typeSelected == CreatureType.SLUG)
            {
                if (EnergyPollutionManager.energyValue > 50)
                {
                    getNode(activeNode).incrementCreatureAmount((int)CreatureType.SLUG, 1);
                    creatureCreateEvent(DataManager.creatureLookupTable.creatureData[(int)CreatureType.SLUG].energyCostPerSpawn);
                    return;
                }
            }

            if (getNode(activeNode) != null && getNode(activeNode).deletable)
                getNode(activeNode).type = typeSelected;
        }

        /**
        *<summary>
        *Returns a <see cref="NodeModel"/> from a given set of indeces from the Nodes list
        *</summary>
        */
        public static NodeModel getNode(int xIndex, int zIndex)
        {
            if (xIndex < 0 || xIndex > MapWidth - 1 || zIndex < 0 || zIndex > MapLength - 1)
                return null;
            return nodeMaster.nodes[xIndex, zIndex];
        }

        /**
        *<summary>
        *Returns a <see cref="NodeModel"/> from a given nodePos from the Nodes list
        *</summary>
        */
        public static NodeModel getNode(NodePosition nodePos)
        {
            if (!nodePos.inRange)
                return null;
            return nodeMaster.nodes[nodePos.xIndex, nodePos.zIndex];
        }

        /**
        *<summary>
        *Takes a <see cref="NodePosition"/> and, if the position is in range and unoccupied, instantiates a node prefab
        *at that position and inserts it into the Nodes array
        *</summary>
        */
        void spawnNode(NodePosition nodePos)
        {
            if (nodePos.inRange)
            {
                //Exit if there is already a Node at the given input position
                if (getNode(nodePos) != null)
                {
                    return;
                }

                //Exits if the player doesn't have enough energy to spawn a node
                if(EnergyPollutionManager.energyValue < energyCostPerTile)
                {
                    messageInterface.DisplayMessage("Not enough energy. Try growing your population to increase energy!", 4f);
                    return;
                }

                nodeCreateEvent(nodePos, energyCostPerTile);

                GameObject newNode = Instantiate(nodePrefab, nodePos.position, Quaternion.identity) as GameObject;
                NodeModel newNodeModel = newNode.GetComponent<NodeModel>();

                //Null test
                //Should only pop if the Prefabs get messed up
                if (newNodeModel == null)
                {
                    Debug.LogError("The nodePrefab does not have a nodeModel component");
                    Destroy(newNode);
                    return;
                }
                //Puts the new node's NodeModel in the node array
                //and updates activeNode
                nodes[nodePos.xIndex, nodePos.zIndex] = newNodeModel;
                newNodeModel.init(nodePos, new int[10], true);
                activeNode = nodePos;
                activeNodeUpdateEvent(activeNode);
            }
        }
        /**
        *<summary>
        *Takes a <see cref="NodePosition"/> and, if the position is in range and occupied, removes the node
        *at that position and removes it from the Nodes array
        *</summary>
        */
        void deleteNode(NodePosition nodePos)
        {
            //Exit if there is no node at the given index
            if(getNode(nodePos) == null)
            {
                return;
            }

            //Exit if the node has been locked
            //(Initial level placement for instance)
            if(!getNode(nodePos).deletable)
            {
                return;
            }

            getNode(nodePos).deleteNode();
            nodeDeleteEvent(nodePos);
            nodes[nodePos.xIndex, nodePos.zIndex] = null;
        }

        void ToggleRoad(NodeModel nodeMod)
        {
            if (!nodeMod.roadEnabled)
            {
                if (EnergyPollutionManager.energyValue >= energyCostPerRoad)
                {
                    nodeMod.toggleRoadEnabled();
                }
            }
            else
            {
                nodeMod.toggleRoadEnabled();
            }
            roadToggleEvent(nodeMod.roadEnabled, energyCostPerRoad);
        }

        /**
        *<summary>
        *Sets static NodeManager fields based on EditorExposed object values
        *</summary>
        */
        void init()
        {
            //If two or more NodeManagers exist in the scene destroys the secondary instances
            if (nodeMaster != null)
            {
                Destroy(this);
            }
            //Updates the static reference to a NodeManager
            nodeMaster = this;

            //Ensures that MapWidth and MadLength are even values regardless of Inspector assigned values
            MapWidth = _mapWidth % 2 == 0 ? _mapWidth : _mapWidth + 1;
            MapLength = _mapLength % 2 == 0 ? _mapLength : _mapLength + 1;
            //Updates local properties off just defined static properties
            _mapLength = MapLength;
            _mapWidth = MapWidth;

            nodes = new NodeModel[_mapWidth, _mapLength];
            if(genMap)
                pregenMap();
        }

        /**
        *<summary>
        *Randomly assigns a veryLow/low/medium/high/veryHigh amount of vegetation to a randomly determined amount of nodes for initial map generation 
        *</summary>
        */
        void pregenMap()
        {
            int area = _mapWidth * _mapLength;
            int totalTiles = Random.Range(area / 10, area / 5);
            int totalVeg = (int)((int)vegDensity * totalTiles * Random.Range(0.75f, 1f) );
            int vegPerTile = totalVeg / totalTiles;

            int i = 0;

            Debug.Log("Area: " + area + "\nTotal Tiles: " + totalTiles + "\n Total Veg: " + totalVeg + "\nVegetation Per Tile: " + vegPerTile);

            while(i < totalTiles)
            {
                int xPos = Random.Range(0, _mapWidth - 1);
                int zPos = Random.Range(0, _mapLength - 1);

                NodePosition nodePos = new NodePosition(xPos, zPos);

                if(nodes[xPos, zPos] == null)
                {
                    i++;
                    GameObject newNode = Instantiate(nodePrefab, nodePos.position, Quaternion.identity) as GameObject;
                    NodeModel newNodeModel = newNode.GetComponent<NodeModel>();

                    //Null test
                    //Should only pop if the Prefabs get messed up
                    if (newNodeModel == null)
                    {
                        Debug.LogError("The nodePrefab does not have a nodeModel component");
                        Destroy(newNode);
                        return;
                    }
                    //Puts the new node's NodeModel in the node array
                    //and updates activeNode
                    nodes[nodePos.xIndex, nodePos.zIndex] = newNodeModel;
                    newNodeModel.init(nodePos, new int[10], false);
                    activeNode = nodePos;
                    //activeNodeUpdateEvent(activeNode);

                    //Update the vegetation
                    newNodeModel.updateCreatureAmount(0, (int) Mathf.Clamp(vegPerTile * Random.Range(0.5f, 1.5f), 0 , 10));
                }
            }
        }

    }
}
