using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float spd;
    [SerializeField] private float _lookspd;
    private CharacterController _controller;
    private Vector2 _motion;
    private bool _canMove;
    private bool _canLook;
    private Input _input;

    private void Awake(){
        //Initialize input
        _input = new Input();
        //Movement
        _input.Player.Move.performed += ctx => _motion = ctx.ReadValue<Vector2>();
        _input.Player.Move.canceled += ctx => _motion = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start(){
        //Set up default values
        spd = 0.1f;
        _canMove = true;
        _controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(_canMove)
            Move();
    }

    void Update(){
        DrawFront();
    }

    #region InputAction
    private void OnEnable() => _input.Player.Enable();

    private void OnDisable() => _input.Player.Disable();

    #endregion

    #region Movement
    private void Move(){
        var _move = new Vector3(_motion.x, 0, _motion.y); 
        _controller.Move(_move * spd);
    }
    
    public void Look(int dir) =>transform.rotation = Quaternion.Euler(0, dir , 0);

    private void DrawFront(){
        Debug.DrawRay(transform.position, transform.forward * 3, Color.green);
    }
    #endregion
}
