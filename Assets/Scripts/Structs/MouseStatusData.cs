namespace EcoTile
{
    using UnityEngine;

    struct MouseStatusData
    {
        public bool mouse0, mouse0down, mouse0up, mouse1, mouse1down, mouse1up;


        public MouseStatusData(string s)
        {
            mouse0 = Input.GetMouseButton(0);
            mouse0down = Input.GetMouseButtonDown(0);
            mouse0up = Input.GetMouseButtonUp(0);

            mouse1 = Input.GetMouseButton(1);
            mouse1down = Input.GetMouseButtonDown(1);
            mouse1up = Input.GetMouseButtonUp(1);
        }
    }
}
