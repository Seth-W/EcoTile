using UnityEngine;

/**
*<summary>
*Base component class for all entities that can be interacted with in a scene
*Implements <see cref="IObjectModel"/>
*Stores a boolean <see cref="_active"/> and 
*A setActive method which calls Activate/Deactivate accordingly
*</summary>
*/
public abstract class ObjectModel : MonoBehaviour, IObjectModel
{
    bool _active;
    public bool active
    {
        get { return _active; }
        set { setActive(value); }
    }

    IObjectControl control;
    IObjectView view;

    public abstract void Activate();
    public abstract void Deactivate();

    protected virtual void Start()
    {
        assign_MVC_components();
    }

    /**
    *<summary>
    *Called by the set method for <see cref="active"/>
    *If no change to the value the method returns
    *else it changes the value and calls the appropriate Activate/Deactivate method
    *</summary>
    */
    private void setActive(bool b)
    {
        if (_active = b)
            return;
        _active = b;
        if (b)
            Activate();
        else
            Deactivate();
    }
    /**
    *<summary>
    *Uses UnityEngine.GetComponent to assign this gameObject's View and Controller components
    *to the <see cref="control"/> and <see cref="view"/> fields in an ObjectModel
    *</summary>
    */
    protected void assign_MVC_components()
    {
        control = GetComponent<IObjectControl>();
        view = GetComponent<IObjectView>();
    }
}