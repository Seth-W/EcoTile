using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PersistentToggleGroup : MonoBehaviour {

	public bool allowNoSelection;

	private List<PersistentToggle> _toggles;

	private PersistentToggle _selected;

	private List<PersistentToggle> toggles
	{
		get
		{
			if ( _toggles == null )
			{
				_toggles = new List<PersistentToggle>();
			}
			return _toggles;
		}
	}

	public void RegisterToggle ( PersistentToggle argToggle )
	{
		toggles.Add( argToggle );
	}

	public void ClickToggle ( PersistentToggle argToggle )
	{
		if ( !argToggle.selected )
		{
			if ( _selected != null )
			{
				_selected.selected = false;
			}

			argToggle.selected = true;

			_selected = argToggle;
		}
		else if ( allowNoSelection )
		{
			argToggle.selected = false;

			_selected = null;
		}
	}

	public PersistentToggle selected
	{
		get
		{
			return _selected;
		}
	}
}
