using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreatureFeedingSlider : MonoBehaviour {

	public Text nameLabel;

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
