namespace EcoTile.Splines
{
    using UnityEngine;
    using System.Collections.Generic;
    
    class CatmullRom
    {
        public static Vector2 interpolatedPosition(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float i)
        {
            //Queue<Vector2> retValue = new Queue<Vector2>();
            
            float t0 = 0.0f;
            float t1 = GetT(t0, p0, p1);
            float t2 = GetT(t1, p1, p2);
            float t3 = GetT(t2, p2, p3);

            float t = (t2 - t1) * Mathf.Clamp(i, 0f, 1f);
            Debug.Log(t);

            Vector2 a1 = (t1 - t) / (t1 - t0) * p0 + (t- t0) / (t1-t0) * p1;
            Vector2 a2 = (t2 - t) / (t2 - t1) * p1 + (t - t1) / (t2 - t1) * p2;
            Vector2 a3 = (t3 - t) / (t3 - t2) * p2 + (t - t2)/(t3 - t2) * p3;

            Vector2 b1 = (t2 - t) / (t2 - t0) * a1 + (t - t0) / (t1 - t0) * a2;
            Vector2 b2 = (t3 - t) / (t3 - t1) * a2 + (t - t1) / (t2 - t1)* a3;

            Vector2 c = (t2 - t) / (t2 -t1) * b1 + (t - t1) / (t2 - t1) * b2;

            return c;
        }

        public static Vector2 interpolatedPosition_Continuous(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float i)
        {
            Debug.Log(i);
            
            Vector2 a = 2 * p0;
            Vector2 b = (-p0 + p2) * i;
            Vector2 c = (2 * p0 - 5 * p1 + 4 * p2 - p3) * i * i;
            Vector2 d = (-p0 + 3 * p1 - 3 * p2 + p3) * i * i * i;

            return 0.5f * (a + b + c + d);
        }

        public static Vector3 returnCatmullRom(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Vector3 a = 0.5f * (2f * p1);
            Vector3 b = 0.5f * (p2 - p0);
            Vector3 c = 0.5f * (2f * p0 - 5f * p1 + 4f * p2 - p3);
            Vector3 d = 0.5f * (-p0 + 3f * p1 - 3f * p2 + p3);

            Vector3 pos = a + (b * t) + (c * t * t) + (d * t * t * t);

            return pos;
        }

        public static Vector2 returnCatmullRom(float t, Vector2 n0, Vector2 n1, Vector2 n2, Vector2 n3)
        {
            Vector3 p0 = new Vector3(n0.x, 0, n0.y);
            Vector3 p1 = new Vector3(n1.x, 0, n1.y);
            Vector3 p2 = new Vector3(n2.x, 0, n2.y);
            Vector3 p3 = new Vector3(n3.x, 0, n3.y);

            Vector3 a = 0.5f * (2f * p1);
            Vector3 b = 0.5f * (p2 - p0);
            Vector3 c = 0.5f * (2f * p0 - 5f * p1 + 4f * p2 - p3);
            Vector3 d = 0.5f * (-p0 + 3f * p1 - 3f * p2 + p3);

            Vector3 pos = a + (b * t) + (c * t * t) + (d * t * t * t);

            return new Vector2(pos.x, pos.z);
        }

        static float GetT(float t, Vector2 p0, Vector2 p1)
        {
            float a = Mathf.Pow((p1.x - p0.x), 2.0f) + Mathf.Pow((p1.y - p0.y), 2.0f);
            float b = Mathf.Pow(a, 0.5f);
            float c = Mathf.Pow(b, 1f);

            return (c + t);
        }
    }
}