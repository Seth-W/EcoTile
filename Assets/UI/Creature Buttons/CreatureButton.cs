using UnityEngine;
using UnityEngine.UI;
using EcoTile.ExtensionMethods;
using EcoTile;

namespace EcoTile.UI
{
	class CreatureButton : MonoBehaviour
	{
        public delegate void CreatureButtonClick(CreatureType type);
        public static CreatureButtonClick CreatureButtonClickEvent;

		public int lookupId;
		public PersistentToggle toggle;
		public CreatureFeedingSlider feedingSliderPrefab;
		public Image portraitImage;

		public Text nameLabel;

		[HideInInspector]
		[System.NonSerialized]
		public CreatureButtonGroup group;
        [SerializeField]
        Text populationValueText, energyCostValue;

		private CreaturePopup _popup;

        [SerializeField]
		private CreatureLookupTable _table;

        private CreatureType type{ get { return lookupId.CreatureIndexToEnum(); } }

        private int population = 0;

		private string GetCreatureName ( int argIndex )
		{
			GameObject prefab = _table.creatureData [ argIndex ].creaturePrefab;
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
                init();
			}
			get
			{
				return _table;
			}
		}

        void OnEnable()
        {
            //init();

            NodeModel.CreaturePopulationIncrementEvent += OnCreaturePopulationIncrement;
        }
        void OnDisable()
        {
            NodeModel.CreaturePopulationIncrementEvent -= OnCreaturePopulationIncrement;

        }

        // Use this for initialization
        void Awake ()
		{
			_popup = GetComponentInChildren<CreaturePopup>();
			_popup.gameObject.SetActive( false );
			_popup.button = this;
		}

        void OnCreaturePopulationIncrement(CreatureType populationUpdateType, int incrementValue)
        {

            if (populationUpdateType == type && populationValueText != null)
            {
                population += incrementValue;
                populationValueText.text = population.ToString();
            }
        }

		public void OnToggleChanged ()
		{
            if ( toggle.selected )
			{
				popup.gameObject.SetActive( true );
                CreatureButtonClickEvent(lookupId.CreatureIndexToEnum());
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

        void init()
        {
            for (int i = 0; i < _table.creatureData.Length; i++)
            {
                if (i != lookupId)
                {
                    CreatureFeedingSlider slider = GameObject.Instantiate(feedingSliderPrefab);
                    slider.transform.SetParent(popup.sliderGroup.transform, false);
                    slider.nameLabel.text = GetCreatureName(i);
                    popup.sliderGroup.RegisterSlider(slider.groupedSlider);
                    slider.groupedSlider.init(_table.creatureData[lookupId].energyCostPerTick, lookupId.CreatureIndexToEnum(), i.CreatureIndexToEnum());
                }
            }

            nameLabel.text = GetCreatureName(lookupId);
            populationValueText.text = 0.ToString();
            energyCostValue.text = _table.creatureData[lookupId].energyCostPerTick.ToString();

            portraitImage.sprite = _table.creatureData[lookupId].sprite;
        }

	}
	
}
 