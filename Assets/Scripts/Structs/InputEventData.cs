namespace EcoTile
{
    using UnityEngine;
    using EcoTile.ExtensionMethods;

    struct InputEventData
    {
        public Touch[] touchesThisFrame;

        public MouseStatusData mouseInput;

        /**
        *<summary>
        *The current mouse position in pixel coordinates
        *</summary>
        */
        public Vector3 mousePosition;

        public NodePosition nodePos;

        public Vector3 mouseXZProjection, mouseXZProjectionStepwise;
        
        public InputEventData(string s)
        {
            touchesThisFrame = new Touch[Input.touchCount];
            for (int i = 0; i < Input.touchCount; i++)
            {
                touchesThisFrame[i] = Input.GetTouch(i);
            }

            mouseInput = new MouseStatusData("This doesn't do anything");

            mousePosition = Input.mousePosition;

            mouseXZProjection = mousePosition.MousePickToXZPlane();

            mouseXZProjectionStepwise = mousePosition.MousePickToXZPlaneStepWise();

            nodePos = mouseXZProjectionStepwise.ToNodePosition();
        }
    }
}
