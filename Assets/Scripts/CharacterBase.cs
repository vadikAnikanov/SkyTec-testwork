using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterBase : MonoBehaviour
{
    protected BulletPoolModel _bulletPoolModel;
    protected Animator _animator;
    protected Rigidbody _rb;
    protected Scrollbar _healthScrollBar;
    protected ParticleSystem _particle;

    protected float _timeAfterLastShot = 0;

    protected float _currentHealth;

    protected bool isAlife = true;

    protected bool inGame = false;

    [SerializeField]
    private float _bulletDamage;

    protected virtual void Start()
    {
        _bulletPoolModel = GetComponentInChildren<BulletPoolModel>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _healthScrollBar = GetComponentInChildren<Scrollbar>();
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    protected void Update()
    {
        _timeAfterLastShot += Time.deltaTime;

        HealthBarLookAtCamera();
    }

    public virtual void SetToStartGameState() { }

    public virtual void SetToStopGameState() { }

    protected virtual void CheckHealth() { }

    public void ApplyDamage()
    {
        _particle.Play();
        _currentHealth -= _bulletDamage;
        _healthScrollBar.size = _currentHealth * 0.01f;
    }

    private void HealthBarLookAtCamera()
    {
        if (Camera.main != null)
        {
            _healthScrollBar.transform.LookAt(Camera.main.transform.position);
        }
    }

    public virtual void Shoot()
    {
        _bulletPoolModel.Shoot();
    }
}

