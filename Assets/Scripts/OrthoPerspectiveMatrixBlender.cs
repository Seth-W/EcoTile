namespace EcoTile
{
    using UnityEngine;
    using EcoTile.ExtensionMethods;

    class OrthoPerspectiveMatrixBlender
    {
        public Matrix4x4 ortho, perspective;

        public float fov, near, far, orthographicSize;
        private float aspect;
        private bool _orthoOn;

        public OrthoPerspectiveMatrixBlender(float fov, float far, float near, float orthoSize)
        {
            this.fov = fov;
            this.far = far;
            this.near = near;
            this.orthographicSize = orthoSize;

            aspect = (float)Screen.width / (float)Screen.height;

            ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
            perspective = Matrix4x4.Perspective(fov, aspect, near, far);
            _orthoOn = true;
        }

        /**
        *<summary>
        *Returns the a matrix with values interpolated from the Perspective to Orthographic
        *</summary>
        */
        public Matrix4x4 lerpToOrtho(float t)
        {
            return perspective.MatrixLerp(ortho, t);
        }
        /**
        *<summary>
        *Returns the a matrix with values interpolated from the Orthographic to Perspective
        *</summary>
        */
        public Matrix4x4 lerpToPerspective(float t)
        {
            return ortho.MatrixLerp(perspective, t);
        }
    }
}
