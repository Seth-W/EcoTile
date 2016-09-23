namespace EcoTile
{
    using UnityEngine;
    using EcoTile.ExtensionMethods;

    class NodeManager : MonoBehaviour
    {
        public delegate void activeNodeUpdate(NodePosition nodePos);

        public static activeNodeUpdate activeNodeUpdateEvent;

        [SerializeField]
        int _mapWidth, _mapLength;
        public static int MapWidth, MapLength;

        public static NodeManager nodeMaster;
        public static NodePosition activeNode;

        [SerializeField]
        GameObject nodePrefab;

        public NodeModel[,] nodes;


        void OnEnable()
        {
            InputManager.FrameInputEvent += OnInputEvent;
        }
        void OnDisable()
        {
            InputManager.FrameInputEvent -= OnInputEvent;
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
            NodePosition nodePos = new NodePosition(inputData.mousePosition.MousePickToXZPlaneStepWise());

            //On left clicks spawns a node
            if (inputData.mouseInput.mouse0)
            {
                //Early dev measure to Stopgap not having a Fire State Machine
                if (!Input.GetKey(KeyCode.LeftControl))
                {
                    Debug.LogWarning("This should be removed in place of a Fire State machine");
                    return;
                }
                spawnNode(nodePos);
            }
            //Update the activeNode on RightClicks
            if(inputData.mouseInput.mouse1up && !inputData.mouseInput.mouse0)
            {
                activeNode = nodePos;
                activeNodeUpdateEvent(activeNode);
            }
        }

        /**
        *<summary>
        *Returns a <see cref="NodeModel"/> from a given set of indeces from the Nodes list
        *</summary>
        */
        public static NodeModel getNode(int xIndex, int zIndex)
        {
            return nodeMaster.nodes[xIndex, zIndex];
        }
        /**
        *<summary>
        *Returns a <see cref="NodeModel"/> from a given Vector3 from the Nodes list
        *</summary>
        */
        public static NodeModel getNode(Vector3 vec)
        {
            NodePosition nodePos = new NodePosition(vec);
            return nodeMaster.nodes[nodePos.xIndex, nodePos.zIndex];
        }
        /**
        *<summary>
        *Returns a <see cref="NodeModel"/> from a given nodePos from the Nodes list
        *</summary>
        */
        public static NodeModel getNode(NodePosition nodePos)
        {
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
                if (nodes[nodePos.xIndex, nodePos.zIndex] != null)
                {
                    return;
                }

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
                activeNode = nodePos;
                activeNodeUpdateEvent(activeNode);
            }
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
        }

    }
}
