using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float spd;
    private CharacterController _controller;
    private GameObject _followPos;
    private GameObject _lookAt;
    private Vector2 _lookPos;
    private Vector2 _motion;
    private float _maxClamp;
    private bool _canMove;
    private Input _input;
    
    private void Awake()
    {
        //Initialize input
        _input = new Input();
        //Movement
        _input.Player.Move.performed += ctx => _motion = ctx.ReadValue<Vector2>();
        _input.Player.Move.canceled += ctx => _motion = Vector2.zero;
        //Look
        _input.Player.Look.performed += ctx => _lookPos = ctx.ReadValue<Vector2>();
        _input.Player.Look.canceled += ctx => _lookPos = Vector2.zero;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Set up default values
        spd = 0.1f;
        _maxClamp = 80;
        _canMove = true;
        _lookAt = transform.GetChild(0).gameObject;
        _followPos = transform.GetChild(1).gameObject;
        _controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_canMove)
            Move();
        Look();
    }
    
    #region InputAction
    private void OnEnable() => _input.Player.Enable();

    private void OnDisable() => _input.Player.Disable();

    #endregion

    private void Move()
    {
        var move = new Vector3(_motion.x * spd , 0, _motion.y * spd);
        _controller.Move(move);
    }

    private void Look()
    {
        var pos = _lookAt.transform.position;
        pos+= new Vector3(_lookPos.x,_lookPos.y);
        _lookAt.transform.position = new Vector3(Math.Clamp(pos.x,-_maxClamp,_maxClamp), Math.Clamp(pos.y,-_maxClamp,_maxClamp));
    }
}
