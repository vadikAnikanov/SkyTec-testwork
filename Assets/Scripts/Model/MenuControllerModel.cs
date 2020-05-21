using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControllerModel : MonoBehaviour
{
    private GameObject _gamePanel;
    private GameObject _menuPanel;

    private Text _killCounterText;

    private GameControllerModel _gameControllerModel;

    void Start()
    {
        _gamePanel = GameObject.Find("GamePanel");
        _menuPanel = GameObject.Find("MenuPanel");

        _gamePanel.SetActive(false);

        _gameControllerModel = GameObject.Find("GameController").GetComponent<GameControllerModel>();

        _killCounterText =  TransformFinder.FindDeepChild(transform, "KillCounter").GetComponent<Text>();
    }

    public void OnClickButtonStart()
    {
        _gameControllerModel.StartAllGameEntities();
        _gamePanel.SetActive(true);
        _menuPanel.SetActive(false);
    }

    public void OnClickButtonStop()
    {
        _gameControllerModel.StopAllGameEntities();
        _gamePanel.SetActive(false);
        _menuPanel.SetActive(true);
    }

    public void OnClickButtonQuit()
    {
        _gameControllerModel.Quit();
    }

    public void AddNewKill()
    {
        int currentKillCount = int.Parse(_killCounterText.text);
        currentKillCount++;
        _killCounterText.text = currentKillCount.ToString();
    }

    

}
