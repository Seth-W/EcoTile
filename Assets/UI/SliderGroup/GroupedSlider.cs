using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GroupedSlider : MonoBehaviour {

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
}
