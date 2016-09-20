/**
*<summary>
*Base Model Interface for all objects that the mouse can interact with
*Contains behaviors for Activate/Deactivate
*</summary>
*/
public interface IObjectModel
{
    /**
    *<summary>
    *Called by an ObjectModel Componenet's setActive(true method)
    *</summary>
    */
    void Activate();

    /**
    *<summary>
    *Called by an ObjectModel Componenet's setActive(true method)
    *</summary>
    */
    void Deactivate();
}
