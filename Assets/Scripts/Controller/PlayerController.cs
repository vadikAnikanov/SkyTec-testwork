using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventAggregation;

public class PlayerController : MonoBehaviour
{
    private PlayerModel _playerModel;

    private bool _isNowAiming = false;

    private void Awake()
    {
        EventAggregator.Subscribe<OnStartAllGameEntities>(OnStartAllEntities);
        EventAggregator.Subscribe<OnStopAllGameEntities>(OnStopAllEntities);
    }

    void Start()
    {
        _playerModel = GetComponent<PlayerModel>();
    }

    private void Update()
    {
        if(InputAimJoystick.instance.GetIsPointerDown() && !_isNowAiming)
        {
            OnBeginDragStick();
            _isNowAiming = true;
        }

        if(!InputAimJoystick.instance.GetIsPointerDown() && _isNowAiming)
        {
            OnPointerUpShootButton();
            _isNowAiming = false;
        }
    }

    public void OnPointerUpShootButton()
    {
        _playerModel.Shoot();
    }

    public void OnBeginDragStick()
    {
        _playerModel.BeginDragStick();
    }

    public void OnStartAllEntities(IEventBase eventBase)
    {
        _playerModel.SetToStartGameState();
    }

    public void OnStopAllEntities(IEventBase eventBase)
    {
        _playerModel.SetToStopGameState();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("enemy"))
        {
            _playerModel.SetCurrentTargetToShot(col.transform.position); 
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("enemy"))
        {
            _playerModel.SetCurrentTargetToShot(Vector3.zero);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("bullet"))
        {
            _playerModel.ApplyDamage();
        }
    }
}
