using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasySave : MonoBehaviour
{
    PlayerData _playerData;
    Player _player;

    void Awake()
    {
        _playerData = GameManager.Instance.playerData;
        _player = FindObjectOfType<Player>();
    }

    public void Save()
    { 
        //Save player data
        ES3.Save("Energy", _playerData.Energy);
        ES3.Save("Lives", _playerData.Lives);
        ES3.Save("Fuel", _playerData.Fuel);

        //Save player postion and rotation
        ES3.Save("Player position", _player.transform.position);
        ES3.Save("Player rotation", _player.transform.rotation);

        ES3.Save("Saved Scene", SceneManager.GetActiveScene().name);

        Debug.Log("Save done");
        Debug.Log("Energy: " + _playerData.Energy);
        Debug.Log("Lives: " + _playerData.Lives);
        Debug.Log("Fuel: " + _playerData.Fuel);
        Debug.Log("Position: " + _player.transform.position);
        Debug.Log("Rotation: " + _player.transform.rotation);
    }
}
