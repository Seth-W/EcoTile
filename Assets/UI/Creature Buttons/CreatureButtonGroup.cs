using EcoTile;
using UnityEngine;
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
				CreatureButton button = GameObject.Instantiate( buttonPrefab );
				button.group = this;
				button.lookupId = i;

				button.transform.SetParent( transform, false );

				_buttons [ i ] = button;
			}

		}
	}
}
