using UnityEngine;

public class Energy : MonoBehaviour
{
    Player _player;

    void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player.EnergyCollected();
            Destroy(gameObject);
        }
    }
}
