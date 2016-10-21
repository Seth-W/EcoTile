using UnityEngine;
using System.Collections;

public class Toolbox : MonoBehaviour {

	private PersistentToggleGroup _group;

	// Use this for initialization
	void Awake () {
		_group = GetComponent<PersistentToggleGroup>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown( 0 ) )
		{
			if ( _group.selected != null )
			{
				_group.selected.useTool.Invoke();
			}
		}
	}
}
