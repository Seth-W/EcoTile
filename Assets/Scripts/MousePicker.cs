using UnityEngine;

class MousePicker
{
    IObjectControl pickedThisFrame;
    IObjectControl pickedLastFrame;
    IObjectControl clickedObject;

    bool ignoreMouseUp;

    public MousePicker()
    {
        ignoreMouseUp = false;
    }

    /**
    *<summary>>
    *Calls the appropriate behaviour on <see cref="IObjectControl"/>'s based on the stored data from mouse picks.
    *</summary>
    */
    public void mousePickObjectControl()
    {
        if (mousePick())
        {
            if (callHoverOff())
                Debug.Log(this + " is calling HoverOff for " + pickedLastFrame);
            //callMouseDownRevert();
            callMouseUp();
            callMouseDown();
            callHoverOn();
        }
        pickedLastFrame = pickedThisFrame;
    }

    /**
    *<summary>
    *Mouse Picks from screen point to world space.
    *Returns true if the ray crosses a collider with an <see cref="IObjectControl"/> componenet.
    *Otherwise returns false
    *</summary>
    */
    bool mousePick()
    {
        //Initialize the variables
        //Reset picked this frame to null, will be assigned if raycast is successful
        pickedThisFrame = null;
        RaycastHit hitObject;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Cast the ray (No masks)
        Physics.Raycast(ray, out hitObject, 15);

        //Return false if ray didn't hit anything with a collider
        if(hitObject.transform == null)
        {
            return false;
        }

        //Get the IObjectControl component on the hit object
        //Method will return false if pickedThisFrame == null
        pickedThisFrame = hitObject.transform.gameObject.GetComponent<IObjectControl>();

        if (pickedThisFrame == null)
        {
            Debug.LogError("MousePicker did not pick an object with an IObjectControl Component");
        }

        //Returns true if the object hit has an IObjectControl, else returns false
        return (pickedThisFrame != null);
    }


    /**
    *<summary>
    *Calls <see cref="IObjectControl.HoverOff"/> if pickedThisFrame and pickedLastFrame aren't the same
    *</summary>
    */
    bool callHoverOff()
    {
        if (pickedLastFrame != pickedThisFrame && pickedLastFrame != null)
        {
            //Debug.Log("Calling HoverOff for " + pickedLastFrame);
            pickedLastFrame.HoverOff();
            return true;
        }
        return false;
    }

    /**
    *<summary>
    *Calls <see cref="IObjectControl.PrimaryMouseDownRevert"/> pickedThisFrame and clicked object are different
    *Sets IgnoreMouseUp to true, and clickedObject to false
    *</summary>
    */
    void callMouseDownRevert()
    {
        if (pickedThisFrame != clickedObject && clickedObject != null)
        {
            clickedObject.PrimaryMouseDownRevert();
            ignoreMouseUp = true;
            clickedObject = null;
        }
    }

    /**
    *<summary>
    *Calls <see cref="IObjectControl.PriamryMouseUp"/> if this is the first frame the mouse button
    *was released and if ignoreMouseUp is false
    *</summary>
    */
    void callMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //IgnoreMouseUp is true when
            //--while the mouse is held down--
            //clickedObject != pickedObject
            if (ignoreMouseUp)
            {
                ignoreMouseUp = false;
            }
            else
            {
                if (clickedObject != null)
                {
                    clickedObject.PriamryMouseUp();
                }
                else
                {
                    Debug.LogError("ClickedObject is null");
                }
                clickedObject = null;
            }
        }
    }

    /**
    *<summary>
    *Calls <see cref="IObjectControl.PrimaryMouseDown"/>. Called on the first frame
    *where the mouse button is down. Assigns pickedThisFrame to clickedObject.
    *</summary>
    */
    void callMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedObject = pickedThisFrame;
            if (clickedObject != null)
                clickedObject.PrimaryMouseDown();
            else
                Debug.LogError("ClickedObject is null");
        }
    }

    /**
    *<summary>
    *Calls <see cref="IObjectControl.HoverOn"/> for pickedThisFrame when pickedThisFrame differes from pickedLastFrame
    *</summary>
    */
    void callHoverOn()
    {
        if(pickedThisFrame != pickedLastFrame)
        {
            pickedThisFrame.HoverOn();
        }
    }

}

