namespace EcoTile
{
    using UnityEngine;

    struct InputEventData
    {
        public Touch[] touchesThisFrame;

        public MouseStatusData mouseInput;

        public Vector3 mousePosition;
        
        public InputEventData(string s)
        {
            touchesThisFrame = new Touch[Input.touchCount];
            for (int i = 0; i < Input.touchCount; i++)
            {
                touchesThisFrame[i] = Input.GetTouch(i);
            }

            mouseInput = new MouseStatusData("This doesn't do anything");

            mousePosition = Input.mousePosition;
        }  
    }
}
