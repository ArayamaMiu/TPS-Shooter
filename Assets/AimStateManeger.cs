using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimStateManeger : MonoBehaviour
{
    float _xaxis, _yaxis;
    [SerializeField] float _mouseSencce;
    [SerializeField] Transform _camFollowPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _xaxis += Input.GetAxisRaw("Mouse X") * _mouseSencce;
        _yaxis += Input.GetAxisRaw("Mouse Y") * _mouseSencce;
        _yaxis = Mathf.Clamp(_yaxis, -80, 80);
    }

    private void LateUpdate()
    {
        _camFollowPos.localEulerAngles = new Vector3(_yaxis, _camFollowPos.localEulerAngles.y, _camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _xaxis, transform.eulerAngles.z);
    }
}
