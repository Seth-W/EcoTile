using UnityEngine;
using System.Collections;

public class CreatureFeedingSlider : MonoBehaviour {

	private GroupedSlider _groupedSlider;

	public GroupedSlider groupedSlider
	{
		get
		{
			if ( _groupedSlider == null )
			{
				_groupedSlider = GetComponentInChildren<GroupedSlider>();
			}
			return _groupedSlider;
		}
	}
}
