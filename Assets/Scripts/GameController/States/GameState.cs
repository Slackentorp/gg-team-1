using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for Game States
/// </summary>
public abstract class GameState
{

    protected GameController gm;

    // Subclasses have to implement Tick
    public abstract void Tick();

    // Subclasses can implement the following
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
    public virtual void InternalOnGUI() { }

    protected GameState(GameController gm)
    {
        this.gm = gm;
    }
}
