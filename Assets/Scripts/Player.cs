using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public CharacterController _controller;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _gravity;
    private Vector3 _direction;
    [SerializeField]
    private float _jumpHeight;

    [SerializeField]
    private bool _hanging;
    [SerializeField]
    private Ledge activeLedge;

    [SerializeField]
    public bool _canUseElevator = false; // flipped on and off in Elevator

    [SerializeField]
    public Elevator currentElevator;

    [SerializeField]
    public bool onLadder = false;
    [SerializeField]
    public Ladder activeLadder;
    [SerializeField]
    public bool leavingLader = false;

    PlayerAnimatorState _animState;
    //Animation States
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_ROLL = "Roll";
    const string PLAYER_LEDGEHANG = "Hanging";
    const string PLAYER_CLIMB_UP = "ClimbUp";
    const string PLAYER_LADDER_IDLE = "LadderIdle";
    const string PLAYER_LADDER_CLIMB = "LadderClimb";
    const string PLAYER_LADDER_EXIT = "LadderExit";

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animState = GetComponent<PlayerAnimatorState>();
    }

    #region Platform Hang
    public void GrabLedge(Ledge currentLedge)
    {
        //disable charadcter controller temporarily. 
        _controller.enabled = false;
        _hanging = true;
        activeLedge = currentLedge;
        _animState.ChangeAnimationState(PLAYER_LEDGEHANG);
    }
    public void StandUp() // called when exiting the climb up animation 
    {
        transform.position = activeLedge.StandUpPosition.transform.position;
        _controller.enabled = true;
    }

    void ClimbUp()
    {
        if (_hanging == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _animState.ChangeAnimationState(PLAYER_CLIMB_UP);
            }
        }
    }
    #endregion

    #region Ladder
    public void GrabLadder(Ladder currentLadder)
    {
        onLadder = true;
        activeLadder = currentLadder;
        _animState.ChangeAnimationState(PLAYER_LADDER_IDLE);
        _gravity = 0;
    }

    public void GetOffLadder()
    {
        leavingLader = true;

        if(onLadder == true)
        {
            onLadder = false;
            _controller.enabled = false;
            _gravity = 15;
            _animState.ChangeAnimationState(PLAYER_LADDER_EXIT);
        }
    }

    void LadderMovement()
    {
        if (onLadder == true)
        {
            _speed = 10f;
            float verticalInput = Input.GetAxisRaw("Vertical");
            _direction = new Vector3(0, verticalInput, 0);

            if (verticalInput != 0)
            {
                _animState.ChangeAnimationState(PLAYER_LADDER_CLIMB);
            }
            else if (verticalInput == 0)
            {
                _animState.ChangeAnimationState(PLAYER_LADDER_IDLE);
            }
        }

        _direction.y -= _gravity * Time.deltaTime;
        _controller.Move(_direction * _speed * Time.deltaTime);
    }

    #endregion

    #region Normal Movement
    public void ActivateElevator()
    {
        if (_canUseElevator == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentElevator._activated = true;
            }
        }
    }

    void Movement()
    {
        if (_controller.isGrounded == true && onLadder == false)
        {
            _speed = 25f;
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            _direction = new Vector3(0, 0, horizontalInput);
            // _anim.SetFloat("Move", Mathf.Abs(horizontalInput));

            if (horizontalInput != 0)
            {
                Vector3 facingDirection = transform.localEulerAngles;
                facingDirection.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facingDirection;
                _animState.ChangeAnimationState(PLAYER_RUN);
            }
            else if ((horizontalInput == 0))
            {
                _animState.ChangeAnimationState(PLAYER_IDLE);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpHeight;
                _animState.ChangeAnimationState(PLAYER_JUMP);
            }
        }

        if (_hanging == true || onLadder == true || leavingLader == true)
        {
            //  _anim.SetBool("Jumping", false);
        }

        else
        {
            //  _anim.SetBool("Jumping", !_controller.isGrounded);
        }
        _direction.y -= _gravity * Time.deltaTime;
        _controller.Move(_direction * _speed * Time.deltaTime);

    }
    #endregion

    void Update()
    {
        if(onLadder == false)
        {
            Movement();
            ClimbUp();
            ActivateElevator();
        }
        else if(onLadder == true)
        {
            LadderMovement();
        }
    }
  
}

