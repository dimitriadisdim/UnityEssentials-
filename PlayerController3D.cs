using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float spd;
    private CharacterController _controller;
    private GameObject _lookAt;
    private Vector2 _lookPos;
    private Vector2 _motion;
    private float _raycastRange;
    private bool _canInspect;
    private bool _canMove;
    private Camera _camera;
    private UiManager _ui;
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
        //_input.Player.Look.canceled += ctx => _lookPos = Vector2.zero
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set up default values
        spd = 0.1f;
        _raycastRange = 3;
        _canMove = true;
        _canInspect = true;
        _camera = Camera.main;
        _ui = UiManager.Instance;
        _lookAt = transform.GetChild(0).gameObject;
        _controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_canMove)
            Move();
        if(_canInspect)
            Raycast();
        Look();
    }

    #region InputAction
    private void OnEnable() => _input.Player.Enable();

    private void OnDisable() => _input.Player.Disable();

    #endregion
    #region Movement
    private void Move()
    {
        var cameraTransform = _camera.transform;
        var forward = cameraTransform.forward;
        var right = cameraTransform.right;

        //We dont want to move on the y axis
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //Apply movement
        var move = forward * _motion.y + right * _motion.x;
        _controller.Move(move * spd);
    }

    private void Look()
    {
        // var pos = _lookAt.transform.position;
        // pos+= new Vector3(_lookPos.x,_lookPos.y);
        //_lookAt.transform.position = new Vectosr3(Math.Clamp(pos.x,-_maxClamp,_maxClamp), Math.Clamp(pos.y,-_maxClamp,_maxClamp));
    }
    #endregion
    private void Raycast()
    {
        RaycastHit hit;
        var ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
        var mask = LayerMask.GetMask("Inspect");
        if (Physics.Raycast(ray, out hit, _raycastRange, mask)) {
            var obj = hit.transform.gameObject;
            if(string.IsNullOrEmpty(obj.name))
                return;

            _ui.ShowName(obj.name);

            if (_input.Player.Interact.triggered && obj.GetComponent<IInteract>() != null)
                obj.GetComponent<IInteract>().Interact();
        }else
            _ui.ClearName();


    }
}
