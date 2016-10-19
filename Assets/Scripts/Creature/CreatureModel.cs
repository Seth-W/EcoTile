namespace EcoTile
{
    using UnityEngine;
    using EcoTile.ExtensionMethods;

    class CreatureModel : ObjectModel 
    {
        CreatureType _type;
        public CreatureType type
        { get { return _type; } }

        bool pickedUp;

        Vector3 initialPosition;
        Rigidbody rb;
        HingeJoint hinge;
        Ray planeOfMovement;

        [SerializeField]
        Vector3 hingeOffset;
        [SerializeField]
        GameObject hingePrefab;

        
        void OnEnable()
        {
            //InputManager.FrameInputEvent += OnFrameInput;
        }
        void OnDisable()
        {
            InputManager.FrameInputEvent -= OnFrameInput;
        }

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
            InputManager.FrameInputEvent += OnFrameInput;

            initialPosition = transform.parent.transform.position;

            //rb = transform.parent.gameObject.AddComponent<Rigidbody>();

            //hinge = (Instantiate(hingePrefab, transform.position, Quaternion.identity) as GameObject).GetComponent<HingeJoint>();
            //hinge.connectedBody = rb;
            //hinge.transform.Translate(hingeOffset);

            //planeOfMovement = findOrthogonalPlane();
        }

        public void setDown()
        {
            InputManager.FrameInputEvent -= OnFrameInput;

            transform.parent.transform.position = Input.mousePosition.MousePickToXZPlane();
            pickedUp = false;
            //Destroy(rb);
            //Destroy(hinge.gameObject);
        }

        void OnFrameInput(InputEventData data)
        {
            //hinge.transform.position = mousePickAgainstPlane(planeOfMovement);
            transform.parent.transform.position = data.mousePosition.MousePickToXZPlane(0.1f);
        }

        void OnDrawGizmos()
        {
            //UnityEngine.Gizmos.DrawSphere(Input.mousePosition.MousePickToXZPlane(0.1f), 0.1f);
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
        Vector3 mousePickAgainstPlane(Ray plane)
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