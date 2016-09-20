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
            InputManager.InputReleaseEvent += OnInputReleaseEvent;
        }
        void OnDisable()
        {
            InputManager.InputReleaseEvent -= OnInputReleaseEvent;
        }


        void Start()
        {
            if(nodeMaster != null)
            {
                Destroy(this);
            }

            nodeMaster = this;

            MapWidth = _mapWidth % 2 == 0 ? _mapWidth : _mapWidth + 1;
            MapLength = _mapLength % 2 == 0 ? _mapLength : _mapLength + 1;
            _mapLength = MapLength;
            _mapWidth = MapWidth;

            nodes = new NodeModel[_mapWidth, _mapLength];
        }

        /**
        *<summary>
        *On a mouseUp/fingerUp creates an instance of the nodePrefab at the given index if one doesn't already exist
        *</summary>
        */
        void OnInputReleaseEvent(Vector3 pos)
        {
            //Debug.Log("OnInputReleaseEvent called");

            NodePosition nodePos = pos.ToNodePosition();
            if(nodePos.inRange)
            {
                if (nodes[nodePos.xIndex, nodePos.zIndex] != null)
                {
                    return;
                }
                GameObject newNode = Instantiate(nodePrefab, nodePos.position, Quaternion.identity) as GameObject;
                NodeModel newNodeModel = newNode.GetComponent<NodeModel>();
                if(newNodeModel == null)
                {
                    Debug.LogError("The nodePrefab does not have a nodeModel component");
                    Destroy(newNode);
                    return;
                }
                nodes[nodePos.xIndex, nodePos.zIndex] = newNodeModel;
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




    }
}
