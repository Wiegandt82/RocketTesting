using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    Player _playerMovement;
    void Awake()
    {
        _playerMovement = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerMovement.LifeCollected();
            Destroy(gameObject);
        }
    }
}
