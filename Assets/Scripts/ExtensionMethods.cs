namespace EcoTile.ExtensionMethods
{
    using UnityEngine;

    public static class ExtensionMethods
    {
        /**
        *<summary>
        *Extension method to <see cref="Vector3"/> types.
        *Returns the vector3 where a line drawn from the mouseposition on the given camera crosses the XZ plane
        *</summary>
        */
        public static Vector3 MousePickToXZPlane(this Vector3 input)
        {
            //Debug.Log("Calling mousePickToXZPlane");
            Ray ray = Camera.main.ScreenPointToRay(input);

            float vectorAmplitude = -ray.origin.y / ray.direction.y;
            return ray.origin + vectorAmplitude * ray.direction;
        }

        public static Vector3 MousePickToXZPlane(this Vector3 input, float yOffset)
        {
            Ray ray = Camera.main.ScreenPointToRay(input);

            float vectorAmplitude = -(ray.origin.y - yOffset) / ray.direction.y;
            return ray.origin + vectorAmplitude * ray.direction;
        }

        /**
        *<summary>
        *Extension method to <see cref="Vector3"/> types.
        *Returns the floored vector3 + 0.5 where a line drawn from the mouseposition on the given camera crosses the XZ plane
        *Creates a function where the return values step up at discrete positions
        *</summary>
        */
        public static Vector3 MousePickToXZPlaneStepWise(this Vector3 input)
        {
            Ray ray = Camera.main.ScreenPointToRay(input);
            float vectorAmplitude = -ray.origin.y / ray.direction.y;

            Vector3 retValue = ray.origin + vectorAmplitude * ray.direction;
            int xFloor = Mathf.FloorToInt(retValue.x);
            int zFloor = Mathf.FloorToInt(retValue.z);

            retValue.x = xFloor % 2 == 0 ? xFloor + 1: xFloor;
            retValue.z = zFloor % 2 == 0 ? zFloor + 1 : zFloor;

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

        /**
        *<summary>
        *Extension method to <see cref="Matrix4x4"/> types.
        *Linearly interpolates between every value in two 4x4 Matrices 
        *</summary>
        */
        public static Matrix4x4 MatrixLerp(this Matrix4x4 start, Matrix4x4 end, float t)
        {
            Matrix4x4 retValue = new Matrix4x4();
            for (int i = 0; i < 16; i++)
            {
                retValue[i] = Mathf.Lerp(start[i], end[i], t);
            }
            return  retValue;
        }

        /**
        *<summary>
        *Takes a vector3 in pixel coordinates and normalizes it to [0,1] range
        *</summary>
        */
        public static Vector3 ToNormalizeScreenSpace(this Vector3 pos)
        {
            return new Vector3(pos.x / Screen.width, 0, pos.y / Screen.height);
        }

        public static CreatureType CreatureIndexToEnum(this int index)
        {
            switch(index)
            {
                case 0:
                    return CreatureType.VEGETATION;
                case 1:
                    return CreatureType.TOKU;
                case 2:
                    return CreatureType.FROG;
                case 3:
                    return CreatureType.BOAR;
                case 4:
                    return CreatureType.SHEEP;
                case 5:
                    return CreatureType.CRAB;
                case 6:
                    return CreatureType.LIZARD;
                case 7:
                    return CreatureType.DYLLO;
                case 8:
                    return CreatureType.INSECT;
                case 9:
                    return CreatureType.SLUG;
                default:
                    return CreatureType.VEGETATION;
            }
        }
    }
}