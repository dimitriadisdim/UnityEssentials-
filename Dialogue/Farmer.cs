using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Farmer : Npc
{
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _dialogue = gameObject.GetComponent<Dialogue>();
    }

    protected override void Actions()
    {
        _dialogue.StartDialogue();
        _gameManager.ChangeState("Dialogue");
    }
}
