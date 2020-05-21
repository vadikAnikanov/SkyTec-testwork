using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using EventAggregation;


public class EnemyAIModel : CharacterBase
{

    private NavMeshAgent _navMesh;
    private Visibility _visibility;


    [SerializeField]
    private float _fireDistance;
    [SerializeField]
    private float _reloadTime;
    [SerializeField]
    private float _maxhealth;


   


    protected override void Start()
    {
        base.Start();

        _navMesh = GetComponent<NavMeshAgent>();
        _visibility = GetComponent<Visibility>();

        _currentHealth = _maxhealth;
    }

    void Update()
    {
        base.Update();

        if (inGame)
        {

            if (isAlife)
            {
                MoveShootEnemy();
            }
            else
            {
                if (_navMesh.isOnNavMesh)
                    _navMesh.isStopped = true;
            }

            CheckHealth();
        }
        else
        {
            if (_navMesh.isOnNavMesh)
                _navMesh.isStopped = true;
        }

    }

    private void MoveShootEnemy()
    {
        _navMesh.SetDestination(PlayerModel.playerTr.position);

        if (Vector3.Magnitude(PlayerModel.playerTr.position - transform.position) < _fireDistance)
        {
            Shoot();
        }


        if (_rb.IsSleeping())
        {
            _animator.SetBool("run", false);
            _animator.SetBool("idle", true);
        }
        else
        {
            _animator.SetBool("run", true);
            _animator.SetBool("idle", false);
        }
    }

    protected override void CheckHealth()
    {
        if (_currentHealth < 0)
        {
            OnNewKillEvent onNewKillEvent = new OnNewKillEvent();
            EventAggregator.Publish(onNewKillEvent);

            isAlife = false;

            _currentHealth = _maxhealth;
            _healthScrollBar.size = _currentHealth * 0.01f;

            _navMesh.Warp(new Vector3(0, -10, 0));
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

        if (_rb.IsSleeping())
        {
            _animator.SetTrigger("idleToshot");
        }
        else
        {
            _animator.SetTrigger("runToshot");

        }

        base.Shoot();
    }

    public void SetVisibility(bool state)
    {
        _visibility.SetVisibility(state);
    }



    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5);
        _navMesh.enabled = true;
        isAlife = true;
        _navMesh.Warp(SpawnPointsList.GetRandomSpawnPoint());
        _currentHealth = 100;
    }

    public override void SetToStartGameState()
    {
        inGame = true;
        _navMesh.isStopped = false;
    }
    public override void SetToStopGameState()
    {
        inGame = false;
        _animator.SetBool("run", false);
        _animator.SetBool("idle", true);
    }

    

}
