using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerModel : CharacterBase
{
    private Transform _playerCamera;
    private Transform _spectatorCamera;
    public static Transform playerTr;

    [SerializeField]
    private float _playerSpeed;
    [SerializeField]
    private float _reloadTime;
    [SerializeField]
    private float _maxhealth;

    private GameObject _aimPlane;

    private Vector3 _currenttargetToShot;

    private float _beginDragSticktime = 0;

    protected override void Start()
    {
        base.Start();

        playerTr = transform;
        _particle = GetComponentInChildren<ParticleSystem>();

        _aimPlane = GameObject.Find("AimPlane");
        _aimPlane.SetActive(false);

        _playerCamera = GameObject.Find("PlayerCamera").GetComponent<Transform>();
        _playerCamera.gameObject.SetActive(false);

        _spectatorCamera = GameObject.Find("SpectatorCamera").GetComponent<Transform>();

        _currentHealth = _maxhealth;

        _currenttargetToShot = new Vector3(0, 0, 0);
    }

    void Update()
    {
        base.Update();

        if (inGame)
        {
            if (isAlife)
            {
                MovePlayer();
                AimPlayer();
            }

            CheckHealth();

            _beginDragSticktime += Time.deltaTime;


        }

    }

    private void MovePlayer()
    {
        Vector3 originalInputVector = Vector3.one;

        originalInputVector = new Vector3(InputMoveJoystick.instance.Horizontal * _playerSpeed, 0, InputMoveJoystick.instance.Vertical * _playerSpeed);

        _rb.velocity = Quaternion.Euler(new Vector3(0, -45, 0)) * originalInputVector;

        if (_rb.velocity != Vector3.zero)
        {
            transform.forward = _rb.velocity;
            _animator.SetBool("run", true);
            _animator.SetBool("idle", false);
        }
        else
        {
            _animator.SetBool("run", false);
            _animator.SetBool("idle", true);
        }
    }

    private void AimPlayer()
    {
        if ((InputAimJoystick.instance.Horizontal > 0.3 || InputAimJoystick.instance.Horizontal < -0.3 ||
                  InputAimJoystick.instance.Vertical > 0.3 || InputAimJoystick.instance.Vertical < -0.3) && _beginDragSticktime >= 0.2f)
        {
            _aimPlane.SetActive(true);

            Vector3 originalAimDirection = new Vector3(InputAimJoystick.instance.Horizontal, 0, InputAimJoystick.instance.Vertical);

            transform.forward = Quaternion.Euler(new Vector3(0, -45, 0)) * originalAimDirection;
        }
        else
        {
            _aimPlane.SetActive(false);
        }
    }

    protected override void CheckHealth()
    {
        if (_currentHealth < 0)
        {
            _playerCamera.gameObject.SetActive(false);
            _spectatorCamera.gameObject.SetActive(true);
            isAlife = false;
            _currentHealth = _maxhealth;
            transform.position = new Vector3(0, -10, 0);
            StartCoroutine("Respawn");
        }
    }

    public override void Shoot()
    {
        if (_timeAfterLastShot < _reloadTime)
        {
            return;
        }
        else
        {
            _timeAfterLastShot = 0;
        }

        if (_rb.velocity != Vector3.zero)
        {
            _animator.SetTrigger("runToshot");
        }
        else
        {
            _animator.SetTrigger("idleToshot");
        }

        if (_currenttargetToShot != Vector3.zero && _beginDragSticktime <= 0.2f)
        {
            transform.forward = _currenttargetToShot - transform.position;
        }

        base.Shoot();
    }

    public void BeginDragStick()
    {
        _beginDragSticktime = 0;
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5);
        _playerCamera.gameObject.SetActive(true);
        _spectatorCamera.gameObject.SetActive(false);

        isAlife = true;

        transform.position = SpawnPointsList.GetRandomSpawnPoint();

        _currentHealth = 100;
        _healthScrollBar.size = _currentHealth * 0.01f;
    }

    public override void SetToStartGameState()
    {
        inGame = true;
        _spectatorCamera.gameObject.SetActive(false);
        _playerCamera.gameObject.SetActive(true);
    }

    public override void SetToStopGameState()
    {
        inGame = false;
        _playerCamera.gameObject.SetActive(false);
        _spectatorCamera.gameObject.SetActive(true);
    }

    public void SetCurrentTargetToShot(Vector3 newTarget)
    {
        _currenttargetToShot = newTarget;
    }



    
}

