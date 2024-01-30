using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    [HideInInspector] public AcitionBaseState _currentState;

    public ReloadState Reload = new ReloadState();
    public DefaultState Default = new DefaultState();

    [SerializeField]public GameObject _currentWeapon;
    [HideInInspector]public WeaponAmmo _ammo;

    AudioSource _audioSource;

    [HideInInspector] public Animator _anim;

    [SerializeField] public MultiAimConstraint _rHandAim;
    [SerializeField] public TwoBoneIKConstraint _lHandIK;

    void Start()
    {
        SwitchState(Default);
        _ammo = _currentWeapon.GetComponent<WeaponAmmo>();
        _audioSource = _currentWeapon.GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(AcitionBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void WeaponReloaded()
    {
        _ammo.Reload();
        SwitchState(Default);
    }

    public void Magout()
    {
        _audioSource.PlayOneShot(_ammo._magOutSound);
    }
    public void Magin()
    {
        _audioSource.PlayOneShot(_ammo._magInSound);
    }

    public void ReleaseSlide()
    {
        _audioSource.PlayOneShot(_ammo._releasSlideSound);
    }
}
