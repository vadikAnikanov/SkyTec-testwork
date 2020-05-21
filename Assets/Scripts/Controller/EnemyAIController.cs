using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventAggregation;

public class EnemyAIController : MonoBehaviour
{
    private EnemyAIModel _enemyAIModel;

    private void Awake()
    {
        EventAggregator.Subscribe<OnStartAllGameEntities>(OnStartAllEntities);
        EventAggregator.Subscribe<OnStopAllGameEntities>(OnStopAllEntities);
    }

    void Start()
    {
        _enemyAIModel = GetComponent<EnemyAIModel>();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("somePlayer") && col.gameObject != gameObject)
        {
            _enemyAIModel.SetVisibility(true);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("bushes"))
        {
            _enemyAIModel.SetVisibility(false);
        }

        if (col.CompareTag("grass"))
        {
            _enemyAIModel.SetVisibility(true);
        }

        if (col.CompareTag("somePlayer") && col.gameObject != gameObject)
        {
            _enemyAIModel.SetVisibility(true);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("bullet"))
        {
            _enemyAIModel.ApplyDamage();
        }
    }

    public void OnStartAllEntities(IEventBase eventBase)
    {
        _enemyAIModel.SetToStartGameState();
    }
    public void OnStopAllEntities(IEventBase eventBase)
    {
        _enemyAIModel.SetToStopGameState();
    }
}
