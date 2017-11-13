using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for Game States
/// </summary>
public abstract class GameState
{

    protected GameControllerMain gm;

    // Subclasses have to implement Tick
    public abstract void Tick();

    // Subclasses can implement the following
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    protected GameState(GameControllerMain gm)
    {
        this.gm = gm;
    }
}
