using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventAggregation;

public class GameControllerModel : MonoBehaviour
{
    private MenuControllerModel _menuControllerModel;

    private void Awake()
    {
        EventAggregator.Subscribe<OnNewKillEvent>(OnNewKill);
    }

    private void Start()
    {
        _menuControllerModel = GameObject.Find("/Canvas").GetComponent<MenuControllerModel>();
    }

    private void Update()
    {
        
    }

    public void StartAllGameEntities()
    {
        OnStartAllGameEntities onStartAllGameEntities = new OnStartAllGameEntities();
        EventAggregator.Publish(onStartAllGameEntities);
    }

    public void StopAllGameEntities()
    {
        OnStopAllGameEntities onStopAllGameEntities = new OnStopAllGameEntities();
        EventAggregator.Publish(onStopAllGameEntities);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void OnNewKill(IEventBase eventBase)
    {
        _menuControllerModel.AddNewKill();
    }
}
