using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EasyLoad : MonoBehaviour
{
    PlayerData _playerData;
    Player _player;
    PlayerPanel _playerPanel;

    void Awake()
    {
        _playerData = GameManager.Instance.playerData;
        _playerPanel = FindObjectOfType<PlayerPanel>();
    }

    public void Load()
    {
        _playerData.Energy = ES3.Load<int>("Energy");
        _playerData.Lives = ES3.Load<int>("Lives");
        _playerData.Fuel = ES3.Load<float>("Fuel");

        _playerPanel.UpdateEnergy(); // You should add this method in your PlayerPanel script
        _playerPanel.UpdateLives();

        Debug.Log("Data loaded");
        Debug.Log("Energy: " + _playerData.Energy);
        Debug.Log("Lives: " + _playerData.Lives);
        Debug.Log("Fuel: " + _playerData.Fuel);
    }
}
