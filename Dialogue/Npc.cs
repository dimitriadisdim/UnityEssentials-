using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Npc : MonoBehaviour, IInteractable
{
    protected Dialogue _dialogue;
    protected GameManager _gameManager;
    
    public void Interact()
    {
        // We take the IInteractable that can only run on father
        // And then run another function that can be overrided
        Actions();
    }

    protected abstract void Actions();
}
