using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] Vector3 movementDirection = new Vector3 (0, 0, 0);
    [SerializeField] float movementSpeed = 2f;

    Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        Vector3 endPosition = startingPosition + movementDirection * Mathf.PingPong(Time.time * movementSpeed, 1f);

        transform.position = endPosition;
    }
}
