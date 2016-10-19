namespace EcoTile
{
    using UnityEngine;
    using Splines;
    using ExtensionMethods;

    class CreatureView : MonoBehaviour, IObjectView 
    {
        [SerializeField]
        NodePosition nodePos;

        public bool moveEnabled;
        bool didPathingResetLastFrame;
        
        Renderer rend;
        Transform parentTransform;

        Vector2[] pathKeyPoints;        
        Vector3 nextFramePathPoint;

        int pathingIndex;
        int index0, index1, index2, index3;


        [SerializeField]
        float speedMultiplier;
        [SerializeField]
        float unitsPerSecond;
        [SerializeField]
        float yOffset;
        [SerializeField, Range(0, 0.25f)]
        float lookYOffset;

        float pathingInterpolator;
        float pathingInterpolatorAtLastIndexUpdate;
        float heuristicIteratorIncrement;
        float interpolator;


        void OnEnable()
        {
            rend = GetComponent<Renderer>();
            parentTransform = transform.parent;

            //init();

            CameraView.cameraInZoomEvent += OnZoomBeginEvent;
            CameraView.CameraZoomFinishEvent += OnZoomEndEvent;
        }

        void OnDisable()
        {
            CameraView.cameraInZoomEvent -= OnZoomBeginEvent;
            CameraView.CameraZoomFinishEvent -= OnZoomEndEvent;
        }

        void OnZoomBeginEvent(bool zoomIn)
        {
            setOutlineThickness(0);
        }

        void OnZoomEndEvent(bool zoomIn)
        {
            setOutlineThickness(.001f);
        }

        void Update()
        {
            if(moveEnabled)
                move();
        }


        /*void OnDrawGizmos()
        {
            UnityEngine.Gizmos.color = Color.green;
            UnityEngine.Gizmos.DrawSphere(new Vector3(nodePos.position.x + nextFramePathPoint.x, -lookYOffset, nodePos.position.z + nextFramePathPoint.z), 0.1f);
        }*/

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
            setOutlineColor(Color.black);
            //Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by an ObjectControl on the first frame that the mouse hovers over this gameObject
        *</summary>
        */
        public void OnHoverOn()
        {
            setOutlineColor(Color.green);
            //Debug.Log("Calling onHoverOn for: " + this);
            //Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by and ObjectControl on the first frame that the mouse left clicks down while hovering over this gameObject
        *</summary>
        */
        public void OnPrimaryMouseDown()
        {
            //Debug.LogError("The requested method is not implemented");
            setOutlineColor(Color.cyan);
            moveEnabled = false;
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
            //Debug.LogError("The requested method is not implemented");
            setOutlineColor(Color.black);
        }

        /**
        *<summary>
        *Called by and ObjectControl on the first frame that the mouse left clicks up after left clicking down on this object
        *</summary>
        */
        public void OnPrimaryMouseUp()
        {
            //Debug.LogError("The requested method is not implemented");
            setOutlineColor(Color.green);
            moveEnabled = true;
            //setDown();
        }

        /**
        *<summary>
        *Sets the outline color to a given <see cref="Color"/>
        *</summary>
        */
        public void setOutlineColor(Color newColor)
        {
            rend.material.SetColor("_OutlineColor", newColor);
        }
        /**
        *<summary>
        *Sets the outline color to a given <see cref="Vector4"/>
        *</summary>
        */
        public void setOutlineColor(Vector4 newColor)
        {
            rend.material.SetColor("_OutlineColor", newColor);
        }

        /**
        *<summary>
        *Sets the outline width to a given <see cref="float"/>
        *</summary>
        */
        public void setOutlineThickness(float newThickness)
        {
            rend.material.SetFloat("_Outline", Mathf.Clamp(newThickness, 0, 10));
        }

        /**
        *<summary>
        *Translates and Rotates the parent object based on Catmull-Rom spline calculations from <see cref="findNextPlaceOnPath"/>
        *</summary>
        */
        void move()
        {
            Vector3 newPathValue = findNextPlaceOnPath();
            parentTransform.localPosition = nextFramePathPoint;
            nextFramePathPoint = newPathValue;
            //parentTransform.LookAt(parentTransform.TransformPoint(nextFramePathPoint));
            parentTransform.LookAt(new Vector3(nodePos.position.x + nextFramePathPoint.x, -lookYOffset, nodePos.position.z + nextFramePathPoint.z));
        }

        /**
        *<summary>
        *Calculates an interpolation value and returns the interpolated point along a Catmull-Rom spline defined by four points
        *</summary>
        */
        Vector3 findNextPlaceOnPath()
        {
            //float interpolator = Time.time - timeAtLastIndexUpdate;
            pathingInterpolator += Time.deltaTime * speedMultiplier;
            //float interpolator = pathingInterpolator - pathingInterpolatorAtLastIndexUpdate;
            interpolator += Time.deltaTime * heuristicIteratorIncrement;

            //Ensures a that interpolating b/w two points starts at 0
            if (didPathingResetLastFrame)
            {
                interpolator = 0;
                didPathingResetLastFrame = false;
            }

            //Ensures a that interpolating b/w two points ends at 1
            if (interpolator >= 1)
            {
                //timeAtLastIndexUpdate = (int)Time.time;
                interpolator = 1;
                //pathingInterpolatorAtLastIndexUpdate += 1;
            }           

            //Calculate the position along the spline
            Vector2 nextPos = CatmullRom.returnCatmullRom(interpolator, pathKeyPoints[index0], pathKeyPoints[index1], pathKeyPoints[index2], pathKeyPoints[index3]);

            //Update the pathing indices when interpolator finishes a 0-1 cycle
            if (interpolator >= 1)
            {
                incrementPathingIndex();
            }

            return new Vector3(nextPos.x, yOffset, nextPos.y);
        }

        /**
        *<summary>
        *Updates the values for <see cref="pathingIndex"/> and <see cref="index0"/> through <see cref="index3"/>
        *</summary>
        */
        void incrementPathingIndex()
        {
            if(pathingIndex >= pathKeyPoints.Length - 1)
            {
                pathingIndex = 0;
            }
            else
            {
                pathingIndex += 1;
            }

            assignIndexValues();
            didPathingResetLastFrame = true;
            heuristicIteratorIncrement = calculateHeuristicIncrementValue();
            interpolator = 0;
        }

        /**
        *<summary>
        *Updates the values for <see cref="index0"/> through <see cref="index3"/>
        *</summary>
        */
        void assignIndexValues()
        {
            //Loops the index values around the ends of the array

            if (pathingIndex == 0)
            {
                index0 = pathKeyPoints.Length - 1;
                index1 = pathingIndex;
                index2 = pathingIndex + 1;
                index3 = pathingIndex + 2;
            }
            else if (pathingIndex == pathKeyPoints.Length - 2)
            {
                index0 = pathingIndex - 1;
                index1 = pathingIndex;
                index2 = pathingIndex + 1;
                index3 = 0;
            }
            else if (pathingIndex == pathKeyPoints.Length - 1)
            {
                index0 = pathingIndex - 1;
                index1 = pathingIndex;
                index2 = 0;
                index3 = 1;
            }
            else
            {
                index0 = pathingIndex - 1;
                index1 = pathingIndex;
                index2 = pathingIndex + 1;
                index3 = pathingIndex + 2;
            }
        }

        /**
        *<summary>
        *Generates a random list of Vector2's ranging from -1 to 1 for X and Z coordinates
        *</summary>
        */
        void populatePathKeyPoints()
        {
            int size = (int)(Random.value * 10 + 1);

            if(size < 5)
                size = 5;

            pathKeyPoints = new Vector2[size];

            for (int i = 0; i < size; i++)
            {
                pathKeyPoints[i] = new Vector2(Random.value * 1.6f - 0.8f, Random.value * 1.6f - 0.8f);
            }
        }

        /**
        *<summary>
        *Generates a random list of Vector2's ranging from -1 to 1 for X and Z coordinates
        *The first item will be the x and z coordinate of the passed vector3
        *</summary>
        */
        void populatePathKeyPoints(Vector3 position)
        {
            int size = (int)(Random.value * 10 + 1);

            if (size < 5)
                size = 5;

            pathKeyPoints = new Vector2[size];

            pathKeyPoints[0] = new Vector2(position.x, position.z);

            for (int i = 1; i < size; i++)
            {
                pathKeyPoints[i] = new Vector2(Random.value * 1.6f - 0.8f, Random.value * 1.6f - 0.8f);
            }
        }

        /**
        *<summary>
        *Estimates a distance between two points then determines how often the
        *object will need to step through the interpolator to span that distance 
        *</summary>
        */
        float calculateHeuristicIncrementValue()
        {
            float distance = Vector3.Distance(pathKeyPoints[1], pathKeyPoints[2]);
            if (distance >= 0.45f)
                distance = 0.45f;
            return unitsPerSecond / (distance * 2);
        }

        public void init(float yOffset, NodePosition nodePos)
        {
            this.yOffset = yOffset;
            this.nodePos = nodePos;

            nextFramePathPoint = parentTransform.position;

            populatePathKeyPoints();
            assignIndexValues();
            heuristicIteratorIncrement = calculateHeuristicIncrementValue();

            pathingInterpolator = 0;
            pathingInterpolatorAtLastIndexUpdate = 0;
            if(moveEnabled)
                move();
        }

        /**
        *<summary>
        *Called on <see cref="IObjectControl.PriamryMouseUp"/> restarts the movement algorithm from the XZ position of a mousepick
        *</summary>
        */
        public void setDown()
        {
            populatePathKeyPoints(Input.mousePosition.MousePickToXZPlane());
            assignIndexValues();
            heuristicIteratorIncrement = calculateHeuristicIncrementValue();
            pathingInterpolator = 0;
            pathingInterpolatorAtLastIndexUpdate = 0;
            if (moveEnabled)
                move();
        }
    }
}