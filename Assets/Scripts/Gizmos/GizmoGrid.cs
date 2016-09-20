namespace EcoTile.Gizmos
{
    using UnityEngine;
    using EcoTile.ExtensionMethods;

    public class GizmoGrid : MonoBehaviour
    {
        public Color gridColor;

        [SerializeField]
        GameObject tileIndicator, mouseIndicator;

        [SerializeField]
        int gridLength;

        Vector3 mousePos;

        public void OnDrawGizmos()
        {
            Gizmos.color = gridColor;
            if (gridLength % 2 != 0)
                gridLength += 1;
            int halfGridLength = gridLength / 2;

            for (int i = 0; i < gridLength + 1; i++)
            {
                Vector3 vecStart = new Vector3(-halfGridLength, transform.position.y, i - halfGridLength);
                Vector3 vecEnd = new Vector3(halfGridLength, transform.position.y, i - halfGridLength);
                Gizmos.DrawLine(vecStart, vecEnd);
                vecStart = new Vector3(i - halfGridLength, transform.position.y, -halfGridLength);
                vecEnd = new Vector3(i - halfGridLength, transform.position.y, halfGridLength);
                Gizmos.DrawLine(vecStart, vecEnd);
            }
        }

        void Update()
        {
            mouseIndicator.transform.position = Camera.main.MousePickToXZPlane();
            tileIndicator.transform.position = Camera.main.MousePickToXZPlaneStepWise();
        }

    }
}