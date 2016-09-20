
/**
*<summary>
*Base Control class for all objects that the mouse can interact with
*Contains behaviors  for mouse HoverOn/off MouseUp/Down
*</summary>
*/
public interface IObjectControl
{
    /**
    *<summary>
    *Called on the frame when a ray cast from the mouse's position on the screen first collides with this object
    *</summary>
    */
    void HoverOn();
    /**
    *<summary>
    *Called on the first frame that a ray cast from the mouse's position on the screen no longer collides with this object
    *</summary>
    */
    void HoverOff();
    /**
    *<summary>
    *Called on the first frame that the left mouse button is pressed while a ray cast from the
    *mouse's position on the screen collides with this object
    *</summary>
    */
    void PrimaryMouseDown();
    /**
    *<summary>
    *Called on the first frame that the left mouse button is released after MouseDown() has been called
    *</summary>
    */
    void PriamryMouseUp();
    /**
    *<summary>
    *Called on the first frame for the mouseclicked object that
    *-- while the left mouse button is held down-- the mousepicked object does not equal the mouseclicked object
    *</summary>
    */
    void PrimaryMouseDownRevert();
}

