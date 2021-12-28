using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //speed
    [SerializeField]
    private float _playerSpeed = 2f;
    //gravity
    [SerializeField]
    private float _gravity = -9.81f;
    [SerializeField]
    private float _jumpHeight = 2f;
    //direction
    private Vector3 _direction, _velocity;
    private float _yVelocity;

    private bool _canDoubleJump;
    private bool _jumping;
    private bool _holdingLedge;

    [SerializeField]
    private Transform _rollEndPOS;
    private bool _isRolling;


    [SerializeField]
    private CharacterController _charController;
    private Animator _anim;
    private LedgeGrab _activeLedge;

    void Start()
    {
        if (_charController == null)
        {
            Debug.LogError("Character Controller is null!");
        }

        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is null!");
        }
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_holdingLedge == true)
            {
                _anim.SetTrigger("ClimpLedge");
            }
        }
    }

    private void CalculateMovement()
    {
        //if grounded 
        if (_charController.isGrounded)
        {

            if (_jumping == true)
            {
                _jumping = false;
                _anim.SetBool("Jumping", false);
            }

            float horizontal = Input.GetAxis("Horizontal");
            _anim.SetFloat("Speed", Mathf.Abs(horizontal));
            _direction = new Vector3(0, 0, horizontal);
            _velocity = _direction * _playerSpeed;

            if (horizontal != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && _isRolling == false)
            {
                _anim.SetTrigger("Roll");
                _charController.enabled = false;
                _isRolling = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _anim.SetBool("Jumping", true);
                _jumping = true;
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
                //trigger jump animation
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
            {
                _yVelocity += _jumpHeight;
                _canDoubleJump = false;
            }
            _yVelocity -= _gravity * Time.deltaTime;
        }

        _velocity.y = _yVelocity;
        _charController.Move(_velocity * Time.deltaTime);
    }

    public void GrabLedge(Vector3 handPos, LedgeGrab currentLedge)
    {
        _charController.enabled = false;
        _anim.SetBool("HoldingLedge", true);
        _anim.SetFloat("Speed", 0);
        _anim.SetBool("Jumping", false);
        _holdingLedge = true;
        transform.position = handPos;
        _activeLedge = currentLedge;
    }

    public void SetPlayerPosition()
    {

        transform.position = _activeLedge.GetStandPos();
        _charController.enabled = true;
        _anim.SetBool("HoldingLedge", false);
    }

    public void SetPlayerPositionAfterRoll()
    {

        Debug.Log($"Roll Ending Position: {_rollEndPOS.position}");
        Debug.Log($"Current Player Position: {transform.position}");
        transform.position = _rollEndPOS.position;
        _isRolling = false;
        _charController.enabled = true;
    }
}
