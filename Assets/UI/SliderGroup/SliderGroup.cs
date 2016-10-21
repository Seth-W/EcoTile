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
		//argSlider.slider.onValueChanged.AddListener( delegate { OnSliderValueChanged( argSlider ); } );
		Normalize( null );
	}

	private void OnSliderValueChanged ( GroupedSlider argSlider )
	{
		Normalize( argSlider );
	}

	private void Normalize ( GroupedSlider argFixed )
	{
		// get current total

		float total = 0f;
		float nonFixedTotal = 0f;

		foreach ( GroupedSlider slider in sliders )
		{
			total += slider.slider.value;
			if ( slider != argFixed )
			{
				nonFixedTotal += slider.slider.value;
			}
		}

		if ( total != desiredTotal )
		{
			foreach ( GroupedSlider slider in sliders )
			{
				if ( nonFixedTotal == 0 )
				{
				}
				else
				{
					if ( argFixed == null )
					{
						slider.slider.value = slider.slider.value * desiredTotal / total;
					}
					else
					{
						if ( slider != argFixed )
						{
							slider.slider.value = slider.slider.value * desiredTotal / nonFixedTotal;
						}
					}
				}
			}
		}
	}
}
