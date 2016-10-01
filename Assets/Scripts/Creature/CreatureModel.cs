namespace EcoTile
{
    using UnityEngine;
    using EcoTile.ExtensionMethods;

    class CreatureModel : ObjectModel 
    {
        CreatureType _type;
        public CreatureType type
        {
            get { return _type; }
        }

        bool pickedUp;
        Vector3 initialPosition;
        GameObject parent;
        Rigidbody rb;
        Vector3 lastFrameMousePos;
        Ray planeOfMovement;
        [SerializeField]
        float liftFactor;
        

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
        }

        void Update()
        {
            if(pickedUp)
            {
                //transform.parent.position = rayToPlane(findOrthogonalPlane());
            }
        }

        public void pickUp()
        {
            pickedUp = true;
            initialPosition = transform.position;
            rb = transform.parent.gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            Debug.Log(findOrthogonalPlane());
            InputManager.FrameInputEvent += OnFrameInput;
            lastFrameMousePos = Input.mousePosition.ToNormalizeScreenSpace();
            planeOfMovement = findOrthogonalPlane();
        }

        public void setDown()
        {
            transform.position = initialPosition;
            pickedUp = false;
            Destroy(rb);
            InputManager.FrameInputEvent -= OnFrameInput;
        }

        void OnFrameInput(InputEventData data)
        {
            Vector3 newMousePos = data.mousePosition.ToNormalizeScreenSpace();
            Vector2 difference = new Vector2(newMousePos.x - lastFrameMousePos.x, newMousePos.y - lastFrameMousePos.y);
            lastFrameMousePos = newMousePos;
            rb.AddForce(difference.y * Vector3.up * liftFactor);
            rb.AddForce(difference.x * planeOfMovement.direction * liftFactor);
            Debug.Log(difference);
        }

        /**
        *<summary>
        *Returns a plane defined by a line parallel to the near clipping plane and <see cref="Vector3.up"/>
        *</summary>
        */
        Ray findOrthogonalPlane()
        {
            Vector3 positionRelativeToCamera = Camera.main.transform.InverseTransformPoint(transform.position);

            float slope = -positionRelativeToCamera.z / positionRelativeToCamera.x;
            float b = positionRelativeToCamera.z / (slope * positionRelativeToCamera.x);

            Vector3 retValue = new Vector3(-positionRelativeToCamera.z, 0, positionRelativeToCamera.x);

            return new Ray (new Vector3(0, 0, b), retValue);
        }

        /**
        *<summary>
        *For a plane perpendicular to the XZ plane, returns the point where a ray  
        *from the mouse position crosses the plane
        *</summary>
        */
        Vector3 rayToPlane(Ray plane)
        {
            Ray mousePoint = Camera.main.ScreenPointToRay(Input.mousePosition);

            float intersectXPos = (plane.direction.x * plane.origin.x) + plane.origin.y - (mousePoint.direction.x * mousePoint.origin.x) + mousePoint.origin.y;
            intersectXPos /= (plane.direction.x - mousePoint.direction.x);
            float magnitude = (intersectXPos - mousePoint.origin.x) ;
            magnitude /= mousePoint.direction.x;
            return mousePoint.origin + mousePoint.direction * magnitude;
        }
    }
}