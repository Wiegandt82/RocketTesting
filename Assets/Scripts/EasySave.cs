using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasySave : MonoBehaviour
{
    PlayerData _playerData;

    void Awake()
    {
        _playerData = GameManager.Instance.playerData;
    }

    public void Save()
    { 
        ES3.Save("Energy", _playerData.Energy);
        ES3.Save("Lives", _playerData.Lives);
        ES3.Save("Fuel", _playerData.Fuel);

        Debug.Log("Save done");
        Debug.Log("Energy: " + _playerData.Energy);
        Debug.Log("Lives: " + _playerData.Lives);
        Debug.Log("Fuel: " + _playerData.Fuel);
    }
}
