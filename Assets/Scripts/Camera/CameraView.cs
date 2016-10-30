namespace EcoTile
{
    using UnityEngine;
    using System.Collections;

    class CameraView : MonoBehaviour, IObjectView
    {
        public delegate void CameraInZoom(bool zoomingIn);
        public delegate void CameraZoomFinish(bool zoomingIn);

        public static CameraInZoom cameraInZoomEvent;
        public static CameraZoomFinish CameraZoomFinishEvent;

        [SerializeField]
        float fov, near, far, orthoSize, zoomDuration;

        OrthoPerspectiveMatrixBlender projectionBlender;

        [SerializeField]
        Camera mainCamera;

        bool _inTransition = false;
        bool _zoomedIn = false;

        void OnEnable()
        {
            projectionBlender = new OrthoPerspectiveMatrixBlender(fov, far, near, orthoSize);

            InputManager.FrameInputEvent += OnFrameInput;
        }

        void OnDisable()
        {
            InputManager.FrameInputEvent -= OnFrameInput;
        }

        void OnFrameInput(InputEventData data)
        {
            if (data.toolType == ToolBoxEnum.ZOOM_IN)
                zoomIn();
            if (data.toolType == ToolBoxEnum.ZOOM_OUT)
                zoomOut();
        }

        /**
        *<summary>
        *Called by and ObjectModel when the objectModel Enabled property is set to true
        *</summary>
        */
        public void OnActivate()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by and ObjectModel when the objectModel Enabled property is set to false
        *</summary>
        */
        public void OnDeactivate()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by an ObjectControl on the first frame that the mouse is no longer hovering over this gameObject when it previously had been
        *</summary>
        */
        public void OnHoverOff()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by an ObjectControl on the first frame that the mouse hovers over this gameObject
        *</summary>
        */
        public void OnHoverOn()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by and ObjectControl on the first frame that the mouse left clicks down while hovering over this gameObject
        *</summary>
        */
        public void OnPrimaryMouseDown()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called on the first frame for the mouseclicked object that
        *-- while the left mouse button is held down-- 
        *the mousepicked object does not equal the mouseclicked object
        *</summary>
        */
        public void OnPrimaryMouseDownRevert()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Called by and ObjectControl on the first frame that the mouse left clicks up after left clicking down on this object
        *</summary>
        */
        public void OnPrimaryMouseUp()
        {
            Debug.LogError("The requested method is not implemented");
        }

        /**
        *<summary>
        *Transitions the camera from a zoomed-out state to the zoomed-in state
        *</summary>
        */
        public void changeZoomState()
        {
            if (_zoomedIn)
                zoomOut();
            else
                zoomIn();
        }

        /**
        *<summary>
        *Initiates a coroutine to transition the camera from a zoomed-out state to the zoomed-in state
        *</summary>
        */
        private void zoomIn()
        {
            if (_inTransition || _zoomedIn)
                return;

            _inTransition = true;
            _zoomedIn = true;

            cameraInZoomEvent(_zoomedIn);
            StartCoroutine(coZoomOrthoToPersp(zoomDuration));
        }

        /**
        *<summary>
        *Initiates a coroutine to transition the camera from a zoomed-in state to the zoomed-out state
        *</summary>
        */
        private void zoomOut()
        {
            if (_inTransition || !_zoomedIn)
                return;

            _inTransition = true;
            _zoomedIn = false;

            cameraInZoomEvent(_zoomedIn);
            StartCoroutine(coZoomPerspToOrtho(zoomDuration));
        }

        /**
        *<summary>
        *Transitions the camera from a zoomed-out state to the zoomed-in state
        *</summary>
        */
        private IEnumerator coZoomIn(float duration)
        {
            float startTime = Time.time;
            float i = (Time.time - startTime) / duration;

            Vector3 cameraHolderEndPos = NodeManager.activeNode.position;

            while (Time.time - startTime < duration)
            {
                i = (Time.time - startTime) / duration;

                transform.position = Vector3.Lerp(Vector3.zero, cameraHolderEndPos, i);
                mainCamera.transform.localPosition = Vector3.Lerp(new Vector3(-30, 25, -30), new Vector3(-2.5f, 2.5f, -2.5f), Mathf.Clamp(i * 2, 0, 1));
                mainCamera.orthographicSize = Mathf.Lerp(10, 0.5f, i);
                Debug.Log(Time.time - startTime);
                yield return 1;
            }
            mainCamera.orthographicSize = 0.5f;
            transform.position = cameraHolderEndPos;
            mainCamera.transform.localPosition = new Vector3(-2.5f, 2.5f, -2.5f);

            _inTransition = false;
            CameraZoomFinishEvent(_zoomedIn);
        }

        /**
        *<summary>
        *Transitions the camera from a zoomed-out state to the zoomed-in state
        *</summary>
        */
        private IEnumerator coZoomOut(float duration)
        {
            float startTime = Time.time;
            float i = (Time.time - startTime) / duration;
            Vector3 cameraHolderStartPos = transform.position;

            while (Time.time - startTime < duration)
            {
                i = (Time.time - startTime) / duration;

                transform.position = Vector3.Lerp(cameraHolderStartPos, Vector3.zero, i);
                mainCamera.transform.localPosition = Vector3.Lerp(new Vector3(-2.5f, 2.5f, -2.5f), new Vector3(-30, 25, -30), Mathf.Clamp(i * 2, 0, 1));
                mainCamera.orthographicSize = Mathf.Lerp(0.5f, 10, i);
                Debug.Log(Time.time - startTime);
                yield return 1;
            }
            mainCamera.orthographicSize = 10f;
            transform.position = Vector3.zero;
            mainCamera.transform.localPosition = new Vector3(-30, 25, -30);

            _inTransition = false;
            CameraZoomFinishEvent(_zoomedIn);
        }


        /**
        *<summary>
        *Transitions the camera from a zoomed-out state to the zoomed-in state
        *Changes positions and perspective styles
        *</summary>
        */
        private IEnumerator coZoomOrthoToPersp(float duration)
        {
            float startTime = Time.time;
            float i = (Time.time - startTime) / duration;

            Vector3 cameraHolderEndPos = NodeManager.activeNode.position;
            cameraHolderEndPos.y = 0;

            while (Time.time - startTime < duration)
            {
                i = (Time.time - startTime) / duration;

                transform.position = Vector3.Lerp(Vector3.zero, cameraHolderEndPos, 4 * i - 3);
                mainCamera.transform.localPosition = Vector3.Lerp(new Vector3(-30, 25, -30), new Vector3(-3f, 2.5f, -3f), i);
                mainCamera.projectionMatrix = projectionBlender.lerpToPerspective(i);
                yield return null;
            }
            transform.position = cameraHolderEndPos;
            mainCamera.transform.localPosition = new Vector3(-3f, 2.5f, -3f);
            mainCamera.projectionMatrix = projectionBlender.perspective;
            _inTransition = false;
            CameraZoomFinishEvent(_zoomedIn);
            setProjectionMatrix();
        }

        /**
        *<summary>
        *Transitions the camera from a zoomed-in state to the zoomed-out state
        *Changes positions and perspective styles
        *</summary>
        */
        private IEnumerator coZoomPerspToOrtho(float duration)
        {
            float startTime = Time.time;
            float i = (Time.time - startTime) / duration;

            Vector3 cameraHolderStartPos = transform.position;

            while(Time.time - startTime < duration)
            {
                i = (Time.time - startTime) / duration;
                
                transform.position = Vector3.Lerp(cameraHolderStartPos, Vector3.zero, 4 * i);
                mainCamera.transform.localPosition = Vector3.Lerp(new Vector3(-3f, 2.5f, -3f), new Vector3(-30, 25, -30),i);
                mainCamera.projectionMatrix = projectionBlender.lerpToOrtho(i);
                yield return null;
            }

            transform.position = Vector3.zero;
            mainCamera.transform.localPosition = new Vector3(-30, 25, -30);
            mainCamera.projectionMatrix = projectionBlender.ortho;
            _inTransition = false;
            CameraZoomFinishEvent(_zoomedIn);
            setProjectionMatrix();
        }

        /**
        *<summary>
        *Sets the appropriate properties in the <see cref="mainCamera"/> object
        *Then calls Unity's <see cref="Camera.ResetProjectionMatrix"/> so <see cref="Camera.ScreenPointToRay(Vector3)"/> plays nice
        *</summary>
        */
        private void setProjectionMatrix()
        {
            if (_zoomedIn)
            {
                mainCamera.orthographic = false;
                mainCamera.fieldOfView = fov;
            }
            else
            {
                mainCamera.orthographic = true;
                mainCamera.orthographicSize = orthoSize;
            }
            mainCamera.farClipPlane = far;
            mainCamera.nearClipPlane = near;
            mainCamera.ResetProjectionMatrix();
        }

    }
}