using UnityEngine;

public class EasyLoad : MonoBehaviour
{
    PlayerData _playerData;
    Player _player;
    PlayerPanel _playerPanel;

    void Awake()
    {
        _playerData = GameManager.Instance.playerData;
        _playerPanel = FindObjectOfType<PlayerPanel>();
        _player = FindObjectOfType<Player>();
    }

    public void Load()
    {
        //Load player data
        _playerData.Energy = ES3.Load<int>("Energy");
        _playerData.Lives = ES3.Load<int>("Lives");
        _playerData.Fuel = ES3.Load<float>("Fuel");

        // Load player position
        Vector3 playerPosition = ES3.Load<Vector3>("Player position");
        _player.transform.position = playerPosition;
        //Load player rotation
        Quaternion playerRotation = ES3.Load<Quaternion>("Player rotation");
        _player.transform.rotation = playerRotation;

        // Updating PlayerPanel 
        _playerPanel.UpdateEnergy(); 
        _playerPanel.UpdateLives();

        Debug.Log("Data loaded");
        Debug.Log("Energy: " + _playerData.Energy);
        Debug.Log("Lives: " + _playerData.Lives);
        Debug.Log("Fuel: " + _playerData.Fuel);
    }
}
