using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    Player player;
    public Player Player { get { return player; } }

    private void Awake()
    {
        if (Instance != null) //If instance has been set then destroy new one
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); //this will prevent GameManager instance to be destroyed when level is being loaded

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int buildIndex = scene.buildIndex;
        if (buildIndex >= 1 && buildIndex <= 21) // Assuming game scenes have build indices from 1 to 21
        {
            player = FindObjectOfType<Player>();
            if (player != null)
            {
                BindPlayerDataToPlayer(player);
            }
        }
    }

    public void BindPlayerDataToPlayer(Player player)
    {
        player.BindPlayerData(playerData);
    }

    [SerializeField] public PlayerData playerData = new PlayerData(); // Reference to the player data, [SerializeField] + [Serializeable] in player data class will allow to see it in inspector

    // OnDestroy method will unsubscribe from the sceneLoaded event
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static void EnsureInstanceExists()
    {
        if (Instance == null)
        {
            GameObject gameManagerObject = new GameObject("GameManager");
            gameManagerObject.AddComponent<GameManager>();
        }
    }
}
