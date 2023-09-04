using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacle : MonoBehaviour
{
    [SerializeField] Vector3 rotationDirection = new Vector3 (0, 0, 0);

    [SerializeField] float rotationSpeed = 10f;
   
    void Update()
    {
        transform.Rotate (rotationDirection * rotationSpeed * Time.deltaTime);
    }
}
