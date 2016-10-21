using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SliderGroup : MonoBehaviour {

	public float desiredTotal;

	private List<GroupedSlider> _sliders;

	private List<GroupedSlider> sliders
	{
		get
		{
			if ( _sliders == null )
			{
				_sliders = new List<GroupedSlider>();
			}
			return _sliders;
		}
	}

	public void RegisterSlider ( GroupedSlider argSlider )
	{
		sliders.Add( argSlider );
		argSlider.slider.onValueChanged.AddListener( delegate { OnSliderValueChanged( argSlider ); } );
		Normalize( argSlider );
	}

	private void OnSliderValueChanged ( GroupedSlider argSlider )
	{
		if ( !argSlider.changing )
		{
			Normalize( argSlider );
		}
	}

	private void Normalize ( GroupedSlider argFixed )
	{
		// get current total

		float total = 0f;
		float nonFixedTotal = 0f;

		foreach ( GroupedSlider slider in sliders )
		{
			slider.changing = true;

			total += slider.slider.value;
			if ( slider != argFixed )
			{
				nonFixedTotal += slider.slider.value;
			}
		}

		if ( total != desiredTotal )
		{

			float totalLeftForNonFixed = desiredTotal - argFixed.slider.value;

			foreach ( GroupedSlider slider in sliders )
			{
				if ( slider != argFixed )
				{
					if ( nonFixedTotal == 0 )
					{
						slider.slider.value = totalLeftForNonFixed / (float) ( sliders.Count - 1 );
					}
					else
					{
						slider.slider.value = slider.slider.value * totalLeftForNonFixed / nonFixedTotal;
					}
				}
			}
		}

		foreach ( GroupedSlider slider in sliders )
		{
			slider.changing = false;
		}
	}
}
