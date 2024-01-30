using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float _firerate;
    [SerializeField] bool _semiAuto;
    float _fireRateTimer;

    [Header("Bullet Properties")]
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _barrelPos;
    [SerializeField] float _bulletVelocity;
    [SerializeField] float _bulletPerShot;
    AimStateManeger aim;

    [SerializeField] AudioClip _gunShot;
    AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManeger>();
        _fireRateTimer = _firerate;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFire()) Fire();
    }

    bool ShouldFire()
    {
        _fireRateTimer += Time.deltaTime;
        if (_fireRateTimer < _firerate) return false;
       
        if (_semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
       
        if (!_semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    void Fire()
    {
        _fireRateTimer = 0;
        _barrelPos.LookAt(aim._aimPos);
        _audioSource.PlayOneShot(_gunShot);

        for (int i = 0; i < _bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(_bullet, _barrelPos.position, _barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(_barrelPos.forward * _bulletVelocity, ForceMode.Impulse);
        }
    }
}
