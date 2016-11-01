using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EcoTile;

public class GroupedSlider : MonoBehaviour {

	[HideInInspector]
	[System.NonSerialized]
	public bool changing;

    private CreatureType _buttonType;
    private CreatureType _sliderType;
    public CreatureType buttonType { get { return _buttonType; } }
    public CreatureType sliderType { get { return _sliderType; } }

	private Slider _slider;

	public Slider slider
	{
		get
		{
			if ( _slider == null )
			{
				_slider = GetComponent<Slider>();
			}
			return _slider;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void init(int maxValue, CreatureType buttonType, CreatureType sliderType)
    {
        slider.wholeNumbers = true;
        slider.minValue = 0;
        slider.maxValue = maxValue;
        _buttonType = buttonType;
        _sliderType = sliderType;
        slider.value = DataManager.creatureLookupTable.creatureData[(int)_buttonType].amountsOfEachToFeed[(int)_sliderType];
    }
}
