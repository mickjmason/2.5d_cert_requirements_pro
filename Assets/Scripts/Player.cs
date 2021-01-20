using Assets.Scripts.UtilClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region Inspector variables
    [Header("Movement")]
    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private float _jumpHeight = 20f;

    [SerializeField]
    private float _startingGravity = 1f;

    [SerializeField]
    private float _boostSpeedMultiplier = 1.5f;
    #endregion

    #region non-inspector private variables
    private Vector3 _direction;
    private CharacterController _characterController;
    private Animator _animator;
    private bool _jumping = true;
    private float _currentGravity;
    private bool _hanging = false;
    private Vector3 _standPosition;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Utils.LogNulls(_characterController);
        _animator = GetComponentInChildren<Animator>();
        Utils.LogNulls(_animator);
        _currentGravity = _startingGravity;
    }



    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
        ProcessHanging();
    }

    #region movement
    private void ProcessMovement()
    {
        if (_characterController.enabled)
        {

            if (_characterController.isGrounded)
            {
                if (_jumping)
                {
                    _jumping = false;
                    _animator.SetBool("IsJumping", false);

                }
                var directionInput = Input.GetAxisRaw("Horizontal");
                _direction = new Vector3(0, 0, directionInput) * _speed;
                _animator.SetFloat("Move", Mathf.Abs(directionInput));

                if (directionInput != 0)
                {
                    Vector3 facing = transform.localEulerAngles;
                    facing.y = _direction.z > 0 ? 0 : 180;
                    transform.localEulerAngles = facing;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {

                    _direction.y += _jumpHeight;
                    _jumping = true;
                    _animator.SetBool("IsJumping", true);
                }

            }

            _direction.y -= _currentGravity * Time.deltaTime;
            _characterController.Move(_direction * Time.deltaTime);
        }

    }

    private void ProcessHanging()
    {
        if (_hanging)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _animator.SetTrigger("LedgeClimb");
                
            }
        }
    }



    public void GrabLedge(Vector3 handPosition,Vector3 finalPosition)
    {
        _standPosition = finalPosition;
        _hanging = true;
        _characterController.enabled = false;
        _animator.SetBool("Hanging", true);
        transform.position = handPosition;
        _jumping = false;
        _animator.SetFloat("Move", 0.0f);
        _animator.SetBool("IsJumping", false);
    }
    #endregion

    #region environment interaction
    #endregion

    #region public methods
    public void StandUp()
    {

        transform.position = _standPosition;
        _animator.SetBool("Hanging", false);
        _hanging = false;
        _jumping = false;
        _direction = Vector3.zero;
        _characterController.enabled = true;
    }

    public void BoostSpeed()
    {
        StartCoroutine(BoostSpeedCoroutine());
    }
    #endregion

    #region Coroutines

    IEnumerator BoostSpeedCoroutine()
    {
        var currentSpeed = _speed;
        _speed *= _boostSpeedMultiplier;
        yield return new WaitForSeconds(5f);
        _speed = currentSpeed;

    }
    #endregion

    #region util methods


    private void SetCharacterDirection(float directionInput)
    {
        Vector3 facingDirection = transform.localEulerAngles;
        
        if(directionInput < 0)
        {
            facingDirection.y = 180;
            
        } else
        {
            facingDirection.y = 0;
        }

        transform.localEulerAngles = facingDirection;
    }
    #endregion
}
