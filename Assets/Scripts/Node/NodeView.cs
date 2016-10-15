namespace EcoTile
{
    using UnityEngine;
    using System.Collections;
    using System;
    using System.Collections.Generic;

    class NodeView : MonoBehaviour, IObjectView
    {
        NodeModel model;

        [SerializeField]
        float creatureYOffset;
        
        [SerializeField]
        TileRoadLookupTable roadLookupTable;

        [SerializeField]
        CreatureLookupTable creatureLookupTable;

        [SerializeField]
        TileRoadType type;
        
        [SerializeField]
        GameObject tile;
        
        [SerializeField]
        GameObject[] vegetation;

        int prevVegNumber;
        VegetationLevel vegLevel;

        int prevCreatureNumber;

        int creatureType;

        Stack<GameObject> creaturesOnTile;

        void Start()
        {

        }

        public void OnEnable()
        {
            //Debug.Log("The NodeView OnEnable was called");
            //Init the properties
            model = GetComponent<NodeModel>();
            creaturesOnTile = new Stack<GameObject>();

            //Subscribe to corresponding NodeModel events
            model.NodeModelCreatureAmountsUpdateEvent += OnNodeModelCreatureAmountsUpdate;
        }

        void OnDisable()
        {
            //Unsubscribe to corresponding NodeModel events
            model.NodeModelCreatureAmountsUpdateEvent -= OnNodeModelCreatureAmountsUpdate;
        }

        /**
        *<summary>
        *Called by and ObjectModel when the objectModel Enabled property is set to true
        *</summary>
        */
        public void OnActivate()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by and ObjectModel when the objectModel Enabled property is set to false
        *</summary>
        */
        public void OnDeactivate()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by an ObjectControl on the first frame that the mouse is no longer hovering over this gameObject when it previously had been
        *</summary>
        */
        public void OnHoverOff()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by an ObjectControl on the first frame that the mouse hovers over this gameObject
        *</summary>
        */
        public void OnHoverOn()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by and ObjectControl on the first frame that the mouse left clicks down while hovering over this gameObject
        *</summary>
        */
        public void OnPrimaryMouseDown()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called on the first frame for the mouseclicked object that
        *-- while the left mouse button is held down-- 
        *the mousepicked object does not equal the mouseclicked object
        *</summary>
        */
        public void OnPrimaryMouseDownRevert()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by and ObjectControl on the first frame that the mouse left clicks up after left clicking down on this object
        *</summary>
        */
        public void OnPrimaryMouseUp()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Initializes the property representing the creature type 
        *</summary>
        */
        public void setCreatureType(int t)
        {
            creatureType = t;
        }

        /**
        *<summary>
        *Returns the property representing the creature type 
        *</summary>
        */
        public int getCreatureType()
        {
            return creatureType;
        }

        /**
        *<summary>
        *Called by NodeModel when the creature amounts array is updated
        *Updates the visual feedback of the node 
        *</summary>
        */
        private void OnNodeModelCreatureAmountsUpdate(int[] updatedAmounts)
        {
            //Debug.LogWarning("The requested method is a stub");
            if(prevVegNumber != updatedAmounts[0])
            {
                updateVegetation(updatedAmounts[0]);
            }
            if(creatureType != 0 && creatureType != updatedAmounts.Length - 1)
            {
                if(prevCreatureNumber != updatedAmounts[creatureType])
                {
                    updateCreatureAmounts(updatedAmounts[creatureType]);
                }
            }
        }

        private void updateCreatureAmounts(int newCreatureAmount)
        {
            if(newCreatureAmount == prevCreatureNumber)
                return;
            else if(newCreatureAmount > prevCreatureNumber)
            {
                while(prevCreatureNumber < newCreatureAmount)
                {
                    creaturesOnTile.Push(Instantiate(creatureLookupTable.CreatureModels[creatureType] as GameObject));
                    prevCreatureNumber += 1;
                    if(creaturesOnTile.Peek() == null)
                    {
                        Debug.LogError("A gameobject was not properly instantiated");
                        return;
                    }
                }
            }
            else
            {
                while(prevCreatureNumber > newCreatureAmount)
                {
                    Destroy(creaturesOnTile.Pop());
                    prevCreatureNumber -= 1;
                }
            }
            positionCharacters();
        }

        /**
        *<summary>
        *Positions the creatures on this tile around a subdivided circle
        *</summary>
        */
        private void positionCharacters()
        {
            Debug.Log("Positioning characters for " + model.nodePos);
            
            GameObject[] creaturesTempArray = new GameObject[creaturesOnTile.Count];
            creaturesOnTile.CopyTo(creaturesTempArray, 0);
            int subdivisions = creaturesTempArray.Length;
            float subdivisionsInRadians = 2 * Mathf.PI / (subdivisions);
            Vector3 newPos = new Vector3();
            
            if(subdivisions == 1)
            {
                newPos = new Vector3(0, creatureYOffset, 0);
                creaturesTempArray[0].transform.SetParent(transform);
                creaturesTempArray[0].transform.localPosition = newPos;
            }
            else
            {
                for (int i = 1; i <= subdivisions; i++)
                {
                    newPos.y = 0;
                    newPos.x = Mathf.Cos(subdivisionsInRadians * i);
                    newPos.z = Mathf.Sin(subdivisionsInRadians * i);

                    newPos *= .75f;
                    newPos.y = creatureYOffset;
                    creaturesTempArray[i - 1].transform.SetParent(transform);
                    creaturesTempArray[i - 1].transform.localPosition = newPos;
                }
            }
        }

        /**
        *<summary>
        *Calculates the new vegetation level from a given integer and if there is a change calls
        *<see cref="drawVeg"/> to change the visual appearance of the vegetation
        *</summary>
        */
        private void updateVegetation(int v)
        {
            prevVegNumber = v;
            VegetationLevel temp = vegLevel;

            if (prevVegNumber == 0)
                vegLevel = VegetationLevel.None;
            else if (prevVegNumber <= (int)VegetationLevel.VeryLow)
                vegLevel = VegetationLevel.VeryLow;
            else if (prevVegNumber <= (int)VegetationLevel.Low)
                vegLevel = VegetationLevel.Low;
            else if (prevVegNumber <= (int)VegetationLevel.Medium)
                vegLevel = VegetationLevel.Medium;
            else if (prevVegNumber <= (int)VegetationLevel.High)
                vegLevel = VegetationLevel.High;


            if (temp != vegLevel)
            {
                drawVeg();
            }
        }

        void drawVeg()
        {
            if(vegLevel == VegetationLevel.None)
            {
                vegetation[0].SetActive(false);
                vegetation[1].SetActive(false);
                vegetation[2].SetActive(false);
                vegetation[3].SetActive(false);
            }
            if (vegLevel == VegetationLevel.VeryLow)
            {
                vegetation[0].SetActive(true);
                vegetation[1].SetActive(false);
                vegetation[2].SetActive(false);
                vegetation[3].SetActive(false);
            }
            if (vegLevel == VegetationLevel.Low)
            {
                vegetation[0].SetActive(true);
                vegetation[1].SetActive(true);
                vegetation[2].SetActive(false);
                vegetation[3].SetActive(false);
            }
            if (vegLevel == VegetationLevel.Medium)
            {
                vegetation[0].SetActive(true);
                vegetation[1].SetActive(true);
                vegetation[2].SetActive(true);
                vegetation[3].SetActive(false);
            }
            if (vegLevel == VegetationLevel.High)
            {
                vegetation[0].SetActive(true);
                vegetation[1].SetActive(true);
                vegetation[2].SetActive(true);
                vegetation[3].SetActive(true);
            }
        }

        /**
        *<summary>
        *Changes the tile Gameobject to the appropriate one and sets the rotation based on its neighbors 
        *</summary>
        */
        public void redrawRoads(NodePosition nodePos)
        {
            Debug.Log("Redraw Roads is called for " + nodePos);
            bool nNeighborEnabled = false, sNeighborEnabled = false, eNeighborEnabled = false, wNeighborEnabled = false;
            int newNeighborWithRoadCount = 0;

            NodeModel temp = NodeManager.getNode(nodePos.xIndex + 1, nodePos.zIndex);

            if (temp != null)
            {
                if (temp.roadEnabled)
                {
                    eNeighborEnabled = true;
                    newNeighborWithRoadCount += 1;
                    Debug.Log("Got here");
                }
            }

            temp = NodeManager.getNode(nodePos.xIndex - 1, nodePos.zIndex);
            if (temp != null)
            {
                if (temp.roadEnabled)
                {
                    wNeighborEnabled = true;
                    newNeighborWithRoadCount += 1;
                    Debug.Log("Got here");
                }
            }

            temp = NodeManager.getNode(nodePos.xIndex, nodePos.zIndex + 1);
            if (temp != null)
            {
                if (temp.roadEnabled)
                {
                    nNeighborEnabled = true;
                    newNeighborWithRoadCount += 1;
                    Debug.Log("Got here");
                }
            }

            temp = NodeManager.getNode(nodePos.xIndex, nodePos.zIndex - 1);
            if (temp != null)
            {
                if (temp.roadEnabled)
                {
                    sNeighborEnabled = true;
                    newNeighborWithRoadCount += 1;
                    Debug.Log("Got here");
                }
            }

            //Debug.Log(newNeighborWithRoadCount);

            type = assignTileRoadType(newNeighborWithRoadCount, nNeighborEnabled, sNeighborEnabled, eNeighborEnabled, wNeighborEnabled);
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
            tile = Instantiate(roadLookupTable.getTile(type)) as GameObject;
            tile.transform.SetParent(transform);
            tile.transform.localPosition = new Vector3(0, 0.5f, 0);
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
            else if (type == TileRoadType.N_E)
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
            if (nNeighbor)
            {
                if (eNeighbor)
                {
                    tile.transform.eulerAngles = new Vector3(0, 90, 0);
                }
                if (wNeighbor)
                {
                    tile.transform.eulerAngles = Vector3.zero;
                }
                Debug.LogError("Neighbor values don't fit with a North-East road");
            }
            else if (sNeighbor)
            {
                if (eNeighbor)
                {
                    tile.transform.eulerAngles = new Vector3(0, 180, 0);
                }
                if (wNeighbor)
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
        TileRoadType assignTileRoadType(int neighborRoadNumbers, bool nNeighbor, bool sNeighbor, bool eNeighbor, bool wNeighbor)
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
                if ((nNeighbor && sNeighbor) || (eNeighbor && wNeighbor))
                    type = TileRoadType.N_S;
                else
                    type = TileRoadType.N_E;
            }
            else if (neighborRoadNumbers == 3)
            {
                type = TileRoadType.N_S_W;
            }
            else
            {
                type = TileRoadType.N_S_E_W;
            }
            if (!model.roadEnabled)
            {
                type = TileRoadType.NO_ROAD;
            }
            return type;
        }
    }
}