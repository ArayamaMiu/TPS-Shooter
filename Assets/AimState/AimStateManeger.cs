using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManeger : MonoBehaviour
{
    AimBaseState _currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();

    [SerializeField] float _mouseSencce;
    [SerializeField] Transform _camFollowPos;
    float _xaxis, _yaxis;

    [HideInInspector] public Animator _anim;
    [HideInInspector] public CinemachineVirtualCamera _vCam;
    public float _adsFov = 40;
    [HideInInspector] public float _hipFov;
    [HideInInspector] public float _currentFov;
    public float _fovSmoothSpeed = 10;

    public Transform _aimPos;
    [HideInInspector] public Vector3 _actualAimPos;
    [SerializeField] float _aimSmoothspeed = 20;
    [SerializeField] LayerMask _aimMask;

    void Start()
    {
        _vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        _hipFov = _vCam.m_Lens.FieldOfView;
        _anim = GetComponent<Animator>();
        SwitchState(Hip);
    }

    // Update is called once per frame
    void Update()
    {
        _xaxis += Input.GetAxisRaw("Mouse X") * _mouseSencce;
        _yaxis -= Input.GetAxisRaw("Mouse Y") * _mouseSencce;
        _yaxis = Mathf.Clamp(_yaxis, -80, 80);

        _vCam.m_Lens.FieldOfView = Mathf.Lerp(_vCam.m_Lens.FieldOfView, _currentFov, _fovSmoothSpeed * Time.deltaTime);

        _currentState.UpdateState(this);

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _aimMask))
        {
            _aimPos.position = Vector3.Lerp(_aimPos.position, hit.point, _aimSmoothspeed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        _camFollowPos.localEulerAngles = new Vector3(_yaxis, _camFollowPos.localEulerAngles.y, _camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _xaxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }
}
