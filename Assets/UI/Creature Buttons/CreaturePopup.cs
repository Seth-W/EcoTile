using UnityEngine;
using System.Collections;

namespace EcoTile.UI
{
	class CreaturePopup : MonoBehaviour
	{
		public delegate void ToolUpdate ( ToolBoxEnum type );
		public static ToolUpdate ToolUpdateEvent;

		public RectTransform border;
		public SliderGroup sliderGroup;

		[HideInInspector]
		[System.NonSerialized]
		public CreatureButton button;

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{
			bool inWindow = 
				RectTransformUtility.RectangleContainsScreenPoint( border, Input.mousePosition ) ||
				RectTransformUtility.RectangleContainsScreenPoint( (RectTransform)transform.parent.transform, Input.mousePosition ); 
			if ( !inWindow )
			{
				gameObject.SetActive( false );
				button.toggle.selected = false;
				ToolUpdateEvent( ToolBoxEnum.SELECT );

			}
		}
	}
}
