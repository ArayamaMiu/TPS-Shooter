using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManeger : MonoBehaviour
{
    public float _moveSpeed = 3f;
    float _hzInput, _vInput;

    [HideInInspector] public Vector3 _dir;

    CharacterController _characterController;
    [SerializeField] float _groundOffset;
    [SerializeField] LayerMask _groundMask;
    Vector3 _spherPos;

    [SerializeField] float _gravity = -9.81f;
    Vector3 _velocity;



    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        GetDirectionMove();
    }



    void GetDirectionMove()
    {
        _hzInput = Input.GetAxis("Horizontal");
        _vInput = Input.GetAxis("Vertical");

        _dir = transform.forward * _vInput + transform.right * _hzInput;

        _characterController.Move(_dir.normalized * _moveSpeed * Time.deltaTime);
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
