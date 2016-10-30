using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageButton : MonoBehaviour {

	public GameObject popup;
	public Text messageTextBox;
	public float messageDuration;

    public void DisplayMessage(string message, float duration)
    {
        popup.SetActive(true);
        messageTextBox.text = message;

        Invoke("HidePopup", duration);
    }

    public void SetMessageText ( string argText )
	{
		messageTextBox.text = argText;
	}

	public void ShowPopup ()
	{
		popup.SetActive( true );

		Invoke( "HidePopup", messageDuration );
	}

	private void HidePopup ()
	{
		popup.SetActive( false );
	}
}
