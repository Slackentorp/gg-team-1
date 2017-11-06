using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for Apartment States
/// </summary>
public abstract class ApartmentState
{

    protected Apartment apartment;

    // Subclasses have to implement Tick
    public abstract void Tick();

    // Subclasses can implement the following
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    protected ApartmentState(Apartment apartment)
    {
        this.apartment = apartment;
    }
}
