using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManeger : MonoBehaviour
{
    public float _currentMoveSpeed;
    public float _walkSpeed = 3,_walkBackspeed = 2;
    public float _runSpeed =7 , _runBackspeed = 5;
    
    [HideInInspector] public float _hzInput, _vInput;
    [HideInInspector] public Vector3 _dir;

    CharacterController _characterController;
    [SerializeField] float _groundOffset;
    [SerializeField] LayerMask _groundMask;
    Vector3 _spherPos;

    [SerializeField] float _gravity = -9.81f;
    Vector3 _velocity;

    MovementBaseState _currentState;

    public IdleState Idle = new IdleState();
    public WalkingState Walk = new WalkingState();
    public RunningState Run = new RunningState();

    [HideInInspector] public Animator _anim;



    void Start()
    {
        _anim = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        GetDirectionMove();

        _anim.SetFloat("hzInput", _hzInput);
        _anim.SetFloat("vInput", _vInput);

        _currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }



    void GetDirectionMove()
    {
        _hzInput = Input.GetAxis("Horizontal");
        _vInput = Input.GetAxis("Vertical");

        _dir = transform.forward * _vInput + transform.right * _hzInput;

        _characterController.Move(_dir.normalized * _currentMoveSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        _spherPos = new Vector3(transform.position.x, transform.position.y - _groundOffset, transform.position.z);
        if (Physics.CheckSphere(_spherPos, _characterController.radius - 0.05f, _groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) _velocity.y += _gravity * Time.deltaTime;
        else if (_velocity.y < 0) _velocity.y = -2;

        _characterController.Move(_velocity * Time.deltaTime);
    }

}
