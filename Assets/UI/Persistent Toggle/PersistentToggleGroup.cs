namespace EcoTile.UI
{
    using UnityEngine;
    using EcoTile;
    using System.Collections.Generic;

    class PersistentToggleGroup : MonoBehaviour
    {
        public delegate void ToolUpdate(ToolBoxEnum type);
        public static ToolUpdate ToolUpdateEvent;

        public bool allowNoSelection;

        private List<PersistentToggle> _toggles;

        private PersistentToggle _selected;

        private List<PersistentToggle> toggles
        {
            get
            {
                if (_toggles == null)
                {
                    _toggles = new List<PersistentToggle>();
                }
                return _toggles;
            }
        }



        void OnEnable()
        {
            CameraView.CameraZoomFinishEvent += OnZoomEndEvent;
        }
        void OnDisable()
        {
            CameraView.CameraZoomFinishEvent -= OnZoomEndEvent;
        }


        void OnZoomEndEvent(bool zoomIn)
        {
            if (!zoomIn)
            {
                ToolUpdateEvent(ToolBoxEnum.SELECT);
                for (int i = 0; i < toggles.Count; i++)
                {
                    if (_toggles[i].toolType == ToolBoxEnum.SELECT)
                        ClickToggle(_toggles[i]);
                }
            }
        }

        public void RegisterToggle(PersistentToggle argToggle)
        {
            toggles.Add(argToggle);
        }

        public void ClickToggle(PersistentToggle argToggle)
        {
            if (!argToggle.selected)
            {
                if (_selected != null)
                {
                    _selected.selected = false;
                }

                argToggle.selected = true;

                _selected = argToggle;

                ToolUpdateEvent(argToggle.toolType);
            }
            else if (allowNoSelection)
            {
                argToggle.selected = false;

                _selected = null;
            }
        }

        public PersistentToggle selected
        {
            get
            {
                return _selected;
            }
        }
    }
}