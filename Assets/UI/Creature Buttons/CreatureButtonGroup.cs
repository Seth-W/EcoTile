using EcoTile;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace EcoTile.UI
{
	class CreatureButtonGroup : MonoBehaviour
	{

		public CreatureButton buttonPrefab;

		[SerializeField]
		CreatureLookupTable table;

		private CreatureButton [] _buttons;

		// Use this for initialization
		void Awake ()
		{
			_buttons = new CreatureButton [ table.table.Length ];
			for ( int i = 0; i < table.table.Length; i++ )
			{
				if ( i == 0 || i == table.table.Length - 1 )
				{
					// insert spacer

					GameObject spacer = new GameObject();
					LayoutElement layout = spacer.AddComponent<LayoutElement>();
					layout.flexibleWidth = 1;

					spacer.transform.SetParent( transform, false );
				}


				CreatureButton button = GameObject.Instantiate( buttonPrefab );
				button.group = this;
				button.lookupId = i;
				button.table = table;

				button.transform.SetParent( transform, false );

				_buttons [ i ] = button;
			}

		}
	}
}
