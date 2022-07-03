using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private PlayerController _player;

    private void Start() => _player = GameObject.Find("Player").GetComponent<PlayerController>();

    public void ChangeState(string state)
    {
        switch (state)
        {
            case "Normal":
                _player.canInteract = true;
                _player.OnEnable();
                break;
            
            case "Dialogue":
                _player.canInteract = false;
                _player.anim.Play("Idle");
                _player.OnDisable();
                break;
            
            default:
                Debug.Log("State is not right");
                break;
        }
    }
}
