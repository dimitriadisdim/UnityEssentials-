using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    [SerializeField] private float spd;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;
    [SerializeField] private float fallingGravityScale;
    private PlayerInput _input;
    private Vector2 _motion;
    private Rigidbody2D _rb;
    private bool _isGrounded;
    //Animation
    private SpriteRenderer _sprite;
    private Animator _anim;
    
    private void Awake()
    {
        //Initialize input
        _input = new PlayerInput();
        //Movement
        _input.Player.Move.performed += ctx => _motion = ctx.ReadValue<Vector2>();
        _input.Player.Move.canceled += ctx => _motion = Vector2.zero;
        //Jump
        _input.Player.Jump.performed += ctx => Jump();
        //Animation
        _anim = gameObject.GetComponent<Animator>();
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        //Set up default values
        spd = 5;
        jumpForce = 20;
        gravityScale = 7;
        fallingGravityScale = 20;
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Check();
        Animation();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Check()
    {
        //Check if onGround
        if (_rb.velocity.y == 0)
            _isGrounded = true;
    }

    #region InputAction
    private void OnEnable() => _input.Player.Enable();

    private void OnDisable() => _input.Player.Disable();

    #endregion
    
    #region Movement
    private void Move()
    {
        var movement = new Vector2(_motion.x, 0) * (spd * Time.deltaTime);
        transform.Translate(movement, Space.World);
    }

    private void Jump()
    {
        if (!_isGrounded)
            return;
        _isGrounded = false; 
        
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        //Super mario jump logic
        if (_rb.velocity.y >= 0)        
            _rb.gravityScale = gravityScale;
        else if (_rb.velocity.y < 0)
            _rb.gravityScale = fallingGravityScale;
    }
    #endregion

    private void Animation()
    {
        //Direction
        if (_motion.x < 0)
            _sprite.flipX = false;
        else if (_motion.x > 0)
            _sprite.flipX = true;
        
        
        //Idle/Run
        if (_motion.x == 0)
            _anim.Play("Idle");
        else 
            _anim.Play("Run");
        
    }
}
