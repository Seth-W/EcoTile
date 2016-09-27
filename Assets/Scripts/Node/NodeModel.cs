namespace EcoTile
{
    using UnityEngine;
    using System.Collections;
    using System;

    class NodeModel : ObjectModel 
    {
        public delegate void creatureAmountsUpdate(int[] updatedAmounts);

        public creatureAmountsUpdate NodeModelCreatureAmountsUpdateEvent;

        [SerializeField]
        int[] _creatureAmounts;

        NodePosition nodePos;

        [SerializeField]
        TileRoadLookupTable roadLookupTable;

        [SerializeField]
        GameObject tile;

        [SerializeField]
        bool _roadEnabled;

        public bool roadEnabled
        {
            get { return _roadEnabled; }
        }

        public int[] creatureAmounts
        {
            get { return _creatureAmounts.Clone() as int[]; }
        }

        int neighborWithRoadCount = 0;


        /**
        *<summary>
        *Called by an ObjectModel Componenet's setActive(true method)
        *</summary>
        */
        public override void Activate()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by an ObjectModel Componenet's setActive(true method)
        *</summary>
        */
        public override void Deactivate()
        {
            Debug.LogError("The requested method is not implemented");
        }

        protected override void Start()
        {
            base.Start();
            _creatureAmounts = new int[10];
        }

        void Update()
        {

        }

        public void init(NodePosition nodePos, int[] initCreatureAmounts)
        {
            this.nodePos = nodePos;

            if (initCreatureAmounts.Length != 10)
            {
                Debug.LogError("The array passed to init is not confiugred properly");
                return;
            }else
            {
                _creatureAmounts = initCreatureAmounts;
            }
        }


        /**
        *<summary>
        *Returns a creature amount at a given index
        *</summary>
        */
        public int getCreatureAmount(int index)
        {
            return _creatureAmounts[index];
        }

        /**
        *<summary>
        *Updates a the creatur amount at a given index
        *</summary>
        */
        public void updateCreatureAmount(int index, int n)
        {
            _creatureAmounts[index] = n;
            NodeModelCreatureAmountsUpdateEvent(_creatureAmounts.Clone() as int[]);
        }

        /**
        *<summary>
        *Takes an array of integers and updates the whole creature index
        *</summary>
        */
        public void updateCreatureAmount(int[] newAmounts)
        {
            _creatureAmounts = newAmounts;
            NodeModelCreatureAmountsUpdateEvent(_creatureAmounts.Clone() as int[]);
        }

        /**
        *<summary>
        *Deletes the game object this node is attached to
        *</summary>
        */
        public void deleteNode()
        {
            Destroy(gameObject);
        }

        /**
        *<summary>
        *
        *</summary>
        */
        public void setRoadEnabled(bool newValue)
        {
            //Early exit if no change to value
            if (roadEnabled == newValue)
                return;


        }

        void redrawRoads()
        {
            bool nNeighborEnabled = false, sNeighborEnabled = false, eNeighborEnabled = false, wNeighborEnabled = false;
            int newNeighborWithRoadCount = 0;

            if (NodeManager.getNode(nodePos.xIndex + 1, nodePos.zIndex).roadEnabled)
            {
                eNeighborEnabled = true;
                newNeighborWithRoadCount += 1;
            }
            if (NodeManager.getNode(nodePos.xIndex - 1, nodePos.zIndex).roadEnabled)
            {
                wNeighborEnabled = true;
                newNeighborWithRoadCount += 1;
            }
            if (NodeManager.getNode(nodePos.xIndex, nodePos.zIndex + 1).roadEnabled)
            {
                nNeighborEnabled = true;
                newNeighborWithRoadCount += 1;
            }
            if (NodeManager.getNode(nodePos.xIndex, nodePos.zIndex - 1).roadEnabled)
            {
                sNeighborEnabled = true;
                newNeighborWithRoadCount += 1;
            }

            TileRoadType type = assignTileRoadType(newNeighborWithRoadCount);
            updateTile(type);
            correctRoadRotation(type, nNeighborEnabled, sNeighborEnabled, eNeighborEnabled, wNeighborEnabled);
        }

        /**
        *<summary>
        *Used to change the tile shown by this node to one of the given <see cref="TileRoadType"/>
        *</summary>
        */
        void updateTile(TileRoadType type)
        {
            Destroy(tile);
            tile = Instantiate(roadLookupTable.getTile(type), new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
        }

        /**
        *<summary>
        *Used to rotate a tile of arbitrary configuration to the correct rotation 
        *</summary>
        */
        void correctRoadRotation(TileRoadType type, bool nNeighbor, bool sNeighbor, bool eNeighbor, bool wNeighbor)
        {
            if (type == TileRoadType.N_S)
            {
                correctNS_RoadRotation(nNeighbor, sNeighbor, eNeighbor, wNeighbor);
            }
            else if(type == TileRoadType.N_E)
            {
                correctNE_RoadRotation(nNeighbor, sNeighbor, eNeighbor, wNeighbor);
            }
            else if (type == TileRoadType.N_S_W)
            {
                correctNSW_RoadRotation(nNeighbor, sNeighbor, eNeighbor, wNeighbor);
            }
        }
        
        /**
        *<summary>
        *Used to rotate a North-South configured tile to the correct rotation 
        *</summary>
        */
        void correctNS_RoadRotation(bool nNeighbor, bool sNeighbor, bool eNeighbor, bool wNeighbor)
        {
            if (nNeighbor || sNeighbor)
            {
                tile.transform.eulerAngles = Vector3.zero;
            }
            else
            {
                tile.transform.eulerAngles = new Vector3(0, 90, 0);
            }
        }

        /**
        *<summary>
        *Used to rotate a North-East configured tile to the correct rotation 
        *</summary>
        */
        void correctNE_RoadRotation(bool nNeighbor, bool sNeighbor, bool eNeighbor, bool wNeighbor)
        {
            if(nNeighbor)
            {
                if(eNeighbor)
                {
                    tile.transform.eulerAngles = new Vector3(0, 90, 0);
                }
                if(wNeighbor)
                {
                    tile.transform.eulerAngles = Vector3.zero;
                }
                Debug.LogError("Neighbor values don't fit with a North-East road");
            }
            else if( sNeighbor)
            {
                if(eNeighbor)
                {
                    tile.transform.eulerAngles = new Vector3(0, 180, 0);
                }
                if(wNeighbor)
                {
                    tile.transform.eulerAngles = new Vector3(0, 270, 0);
                }
                Debug.LogError("Neighbor values don't fit with a North-East road");
            }
        }

        /**
        *<summary>
        *Used to rotate a North-South-West configured tile to the correct rotation 
        *</summary>
        */
        void correctNSW_RoadRotation(bool nNeighbor, bool sNeighbor, bool eNeighbor, bool wNeighbor)
        {
            if (nNeighbor)
            {
                if (sNeighbor)
                {
                    if (eNeighbor)
                    {
                        tile.transform.eulerAngles = Vector3.zero;
                    }
                    if (wNeighbor)
                    {
                        tile.transform.eulerAngles = new Vector3(0, 180, 0);
                    }
                }//End N-S case

                //Test if not E-W case
                else if (!(eNeighbor && wNeighbor))
                {
                    Debug.LogError("Neighbor values don't fit with a N_S_w road");
                }
                else
                {
                    tile.transform.eulerAngles = new Vector3(0, 270, 0);
                }
            }
            else
            {
                tile.transform.eulerAngles = new Vector3(0, 90, 0);
            }
        }

        /**
        *<summary>
        *Sets a TileRoadType enum based on the number of Neighbors
        *</summary>
        */
        TileRoadType assignTileRoadType(int neighborRoadNumbers)
        {
            TileRoadType type;

            if (neighborRoadNumbers == 0)
            {
                type = TileRoadType.NO_NEIGHBOR;
            }
            else if (neighborRoadNumbers == 1)
            {
                type = TileRoadType.N_S;
            }
            else if (neighborRoadNumbers == 2)
            {
                type = TileRoadType.N_S;
            }
            else if (neighborRoadNumbers == 3)
            {
                type = TileRoadType.N_S_W;
            }
            else
            {
                type = TileRoadType.N_S_E_W;
            }
            if (!roadEnabled)
            {
                type = TileRoadType.NO_ROAD;
            }
            return type;
        }
    }
}