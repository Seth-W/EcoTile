namespace EcoTile
{
    using UnityEngine;
    using Splines;

    class CreatureView : MonoBehaviour, IObjectView 
    {
        [SerializeField]
        bool moveEnabled;
        bool didPathingResetLastFrame;
        
        Renderer rend;

        [SerializeField]
        Vector2[] pathKeyPoints;
        
        Vector3 nextFramePathPoint;
        Transform parentTransform;
        
        [SerializeField]
        int pathingIndex;

        [SerializeField]
        float speedMultiplier;
        float lastFrameInterpolator;
        float timeAtLastIndexUpdate;

        float yOffset;

        void OnEnable()
        {
            rend = GetComponent<Renderer>();
            parentTransform = transform.parent;
         
            yOffset = parentTransform.position.y;
            populatePathKeyPoints();
            lastFrameInterpolator = 0.0f;
            nextFramePathPoint = parentTransform.position;
            timeAtLastIndexUpdate = (int)Time.time;

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


        void OnDrawGizmos()
        {
            for (int i = 0; i < pathKeyPoints.Length; i++)
            {
                Vector3 pos = new Vector3 (pathKeyPoints[i].x, .567f, pathKeyPoints[i].y);
                UnityEngine.Gizmos.DrawSphere(pos, 0.1f);
            }
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

        void move()
        {
            Vector3 newPathValue = findNextPlaceOnPath();
            //Debug.Log(nextFramePathPoint);
            parentTransform.localPosition = nextFramePathPoint;
            nextFramePathPoint = newPathValue;
            //parentTransform.LookAt(parentTransform.TransformPoint(nextFramePathPoint));
            parentTransform.LookAt(nextFramePathPoint);
        }

        Vector3 findNextPlaceOnPath()
        {
            float interpolator = Time.time - timeAtLastIndexUpdate;

            //Ensures a that interpolating b/w two points starts at 0
            if (didPathingResetLastFrame)
            {
                interpolator = 0;
                didPathingResetLastFrame = false;
            }

            //Ensures a that interpolating b/w two points ends at 1
            if (interpolator >= 1)
            {
                interpolator = 1;
                timeAtLastIndexUpdate = (int)Time.time;
            }
           
            //Loops the index values around the ends of the array
            int index0, index1, index2, index3;

            if(pathingIndex == 0)
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




            Vector2 nextPos = CatmullRom.returnCatmullRom(interpolator, pathKeyPoints[index0], pathKeyPoints[index1], pathKeyPoints[index2], pathKeyPoints[index3]);

            lastFrameInterpolator = interpolator;
            Vector3 retValue =  new Vector3(nextPos.x, yOffset, nextPos.y);

            if (interpolator >= 1)
            {
                incrementPathingIndex();
            }

            return retValue;
        }

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
            didPathingResetLastFrame = true;
        }

        void populatePathKeyPoints()
        {
            int size = (int)(UnityEngine.Random.value * 10 + 1);

            if(size < 5)
                size = 5;

            pathKeyPoints = new Vector2[size];

            for (int i = 0; i < size; i++)
            {
                pathKeyPoints[i] = new Vector2(UnityEngine.Random.value, UnityEngine.Random.value);
            }
        }
    }
}