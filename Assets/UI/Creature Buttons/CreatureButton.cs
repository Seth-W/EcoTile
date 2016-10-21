using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace EcoTile.UI
{
	class CreatureButton : MonoBehaviour
	{

		public int lookupId;
		public PersistentToggle toggle;
		public CreatureFeedingSlider feedingSliderPrefab;
		public Image portraitImage;

		public Text nameLabel;

		[HideInInspector]
		[System.NonSerialized]
		public CreatureButtonGroup group;

		private CreaturePopup _popup;

		private CreatureLookupTable _table;

		private string GetCreatureName ( int argIndex )
		{
			GameObject prefab = _table.table [ argIndex ].creaturePrefab;
			if ( prefab != null )
			{
				return prefab.name.Replace( "_", "" );
			}
			else
			{
				return "";
			}
		}

		public CreatureLookupTable table
		{
			set
			{
				_table = value;

				for ( int i = 0; i < _table.table.Length; i++ )
				{
					if ( i != lookupId )
					{
						CreatureFeedingSlider slider = GameObject.Instantiate( feedingSliderPrefab );
						slider.transform.SetParent( popup.sliderGroup.transform, false );
						slider.nameLabel.text = GetCreatureName( i );
						popup.sliderGroup.RegisterSlider( slider.groupedSlider );
					}
				}

				nameLabel.text = GetCreatureName( lookupId );

				portraitImage.sprite = _table.table [ lookupId ].sprite;
			}
			get
			{
				return _table;
			}
		}


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
 