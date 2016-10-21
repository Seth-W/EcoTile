using UnityEngine;
using System.Collections;

namespace EcoTile
{
	public class CreaturePopup : MonoBehaviour
	{
		public RectTransform border;

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

			if ( Input.GetMouseButtonDown( 0 ) )
			{
				if ( !RectTransformUtility.RectangleContainsScreenPoint( border, Input.mousePosition ) )
				{
					gameObject.SetActive( false );
					button.toggle.selected = false;
				}
			}

		}
	}
}
