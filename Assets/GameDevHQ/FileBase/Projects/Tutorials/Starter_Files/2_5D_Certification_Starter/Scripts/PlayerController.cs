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
    private bool _climpLadder;

    [SerializeField]
    private Transform _rollEndPOS;
    private bool _isRolling;

    private Ladder _activeLadder;


    [SerializeField]
    private CharacterController _charController;
    private Animator _anim;
    private LedgeGrab _activeLedge;

    [SerializeField]
    private int _coins = 0;

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
        if (_climpLadder == true)
        {
            ClimpLadder();
        }
        else
        {
            CalculateMovement();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_holdingLedge == true)
            {
                _anim.SetTrigger("ClimpLedge");
            }
        }
    }

    private void ClimpLadder()
    {
        float vertical = Input.GetAxis("Vertical");
        //attach speed to animation
        _anim.SetFloat("ClimbLadderSpeed", Mathf.Abs(vertical));
        _direction = new Vector3(0, vertical);
        _velocity = _direction * _playerSpeed;

        _charController.Move(_velocity * Time.deltaTime);
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
        transform.position = _rollEndPOS.position;
        _isRolling = false;
        _charController.enabled = true;
    }

    public void PlayerOnLadder(Vector3 position, Ladder ladder)
    {
        transform.position = position;
        _climpLadder = true;
        _anim.SetBool("ClimbingLadder", true);
        _activeLadder = ladder;

    }

    public void PlayerGetOffLadder()
    {
        Debug.Log("Called PlayerGetOffLadder");
        _charController.enabled = false;
        _anim.SetBool("ClimbOffLadder", true);
    }

    public void PlayerOffLadder()
    { 
        transform.position = _activeLadder.OffLadderPos();
        _climpLadder = false;
        _charController.enabled = true;
        _anim.SetBool("ClimbOffLadder", false);
        _anim.SetBool("ClimbingLadder", false);
    }

    public void AddCoins(int coinAmount)
    {
        _coins += coinAmount;
        UIManager.instance.UpdateCoinText(coinAmount);
    }
}
