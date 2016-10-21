using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class PersistentToggle : MonoBehaviour {

	public Sprite overSprite;
	public Sprite selectedSprite;

	public bool startSelected;

	public UnityEvent changed;

	public UnityEvent useTool;

	private Image _image;
	private Sprite _normalSprite;

	private bool _over;
	private bool _selected;

	private PersistentToggleGroup _group;

	public bool selected
	{
		get
		{
			return _selected;
		}
		set
		{
			_selected = value;
			UpdateSprite();
			changed.Invoke();
		}
	}

	// Use this for initialization
	void Awake () {
		_image = GetComponent<Image>();
		_normalSprite = _image.sprite;
	}

	void Start ()
	{
		_group = GetComponentInParent<PersistentToggleGroup>();

		_group.RegisterToggle( this );

		if ( startSelected )
		{
			OnPointerClick();
		}
	}

	public void OnPointerEnter ()
	{
		_over = true;
		UpdateSprite();
	}

	public void OnPointerExit ()
	{
		_over = false;
		UpdateSprite();
	}

	public void OnPointerClick ()
	{
		_group.ClickToggle( this );
	}

	private void UpdateSprite ()
	{
		if ( selected )
		{
			_image.sprite = selectedSprite;
		}
		else if ( _over )
		{
			_image.sprite = overSprite;
		}
		else
		{
			_image.sprite = _normalSprite;
		}
	}

}
