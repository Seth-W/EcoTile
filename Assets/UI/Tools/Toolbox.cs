namespace EcoTile.UI
{
    using UnityEngine;
    using System.Collections;

    class Toolbox : MonoBehaviour
    {

        private PersistentToggleGroup _group;

        void Awake()
        {
            _group = GetComponent<PersistentToggleGroup>();
        }

        void Update()
        {
            /*if (Input.GetMouseButtonDown(0))
            {
                if (_group.selected != null)
                {
                    _group.selected.useTool.Invoke();
                }
            }*/
        }
    }
}