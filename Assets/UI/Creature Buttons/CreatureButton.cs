using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace EcoTile
{
	public class CreatureButton : MonoBehaviour
	{

		public int lookupId;
		public PersistentToggle toggle;

		[HideInInspector]
		[System.NonSerialized]
		public CreatureButtonGroup group;

		private CreaturePopup _popup;


		// Use this for initialization
		void Awake ()
		{
			_popup = GetComponentInChildren<CreaturePopup>();
			_popup.gameObject.SetActive( false );
			_popup.button = this;
		}

		public void OnToggleChanged ()
		{
			if ( toggle.selected )
			{
				popup.gameObject.SetActive( true );
			}
			else
			{
				popup.gameObject.SetActive( false );
			}
		}

		public CreaturePopup popup
		{
			get
			{
				return _popup;
			}
		}

	}
	
}
 