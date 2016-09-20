/**
*<summary>
*Base Visual interface for all objects that the mouse can interact with
*Contains behaviors to respond to MouseUp/Down & HoverOn/Off events
*Contains behavior to respond to Activate/Deactivate events
*</summary>
*/
public interface IObjectView
{

    /**
    *<summary>
    *Called by an ObjectControl on the first frame that the mouse hovers over this gameObject
    *</summary>
    */
    void OnHoverOn();
    /**
    *<summary>
    *Called by an ObjectControl on the first frame that the mouse is no longer hovering over this gameObject when it previously had been
    *</summary>
    */
    void OnHoverOff();
    /**
    *<summary>
    *Called by and ObjectControl on the first frame that the mouse left clicks down while hovering over this gameObject
    *</summary>
    */
    void OnPrimaryMouseDown();
    /**
    *<summary>
    *Called by and ObjectControl on the first frame that the mouse left clicks up after left clicking down on this object
    *</summary>
    */
    void OnPrimaryMouseUp();
    /**
    *<summary>
    *Called by and ObjectModel when the objectModel Enabled property is set to true
    *</summary>
    */
    void OnActivate();
    /**
    *<summary>
    *Called by and ObjectModel when the objectModel Enabled property is set to false
    *</summary>
    */
    void OnDeactivate();
    /**
    *<summary>
    *Called on the first frame for the mouseclicked object that
    *-- while the left mouse button is held down-- 
    *the mousepicked object does not equal the mouseclicked object
    *</summary>
    */
    void OnPrimaryMouseDownRevert();
}
