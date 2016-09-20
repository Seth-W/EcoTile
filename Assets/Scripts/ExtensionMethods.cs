namespace EcoTile.ExtensionMethods
{
    using UnityEngine;

    public static class ExtensionMethods
    {
        /**
        *<summary>
        *Extension method to <see cref="Camera"/> types.
        *Returns the vector3 where a line drawn from the mouseposition on the given camera crosses the XZ plane
        *</summary>
        */
        public static Vector3 MousePickToXZPlane(this Camera input)
        {
            //Debug.Log("Calling mousePickToXZPlane");
            Ray ray = input.ScreenPointToRay(Input.mousePosition);

            float vectorAmplitude = -ray.origin.y / ray.direction.y;
            return ray.origin + vectorAmplitude * ray.direction;
        }

        /**
        *<summary>
        *Extension method to <see cref="Camera"/> types.
        *Returns the floored vector3 + 0.5 where a line drawn from the mouseposition on the given camera crosses the XZ plane
        *Creates a function where the return values step up at discrete positions
        *</summary>
        */
        public static Vector3 MousePickToXZPlaneStepWise(this Camera input)
        {
            Ray ray = input.ScreenPointToRay(Input.mousePosition);
            float vectorAmplitude = -ray.origin.y / ray.direction.y;

            Vector3 retValue = ray.origin + vectorAmplitude * ray.direction;
            retValue.x = Mathf.Floor(retValue.x) + 0.5f;
            retValue.z = Mathf.Floor(retValue.z) + 0.5f;

            return retValue;
        }

        /**
        *<summary>
        *Extension method to <see cref="Vector3"/> types.
        *Returns a NodePosition struct from the given vector3
        *</summary>
        */
        public static NodePosition ToNodePosition(this Vector3 pos)
        {
            return new NodePosition(pos);
        }
    }
}