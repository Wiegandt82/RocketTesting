using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] float _onInterval = 2f;
    [SerializeField] float _offInterval = 1f;

    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        if (_particleSystem == null)
        {
            Debug.LogError("Particle System not assigned!");
            enabled = false; //Turn off the script
            return;
        }

        StartCoroutine(ControlParticle());
    }

    IEnumerator ControlParticle()
    {
        while (true)
        {
            if (isActive)
            {
                _particleSystem.Stop();
                yield return new WaitForSeconds(_offInterval);
            }
            else
            {
                _particleSystem.Play();
                yield return new WaitForSeconds(_onInterval);
            }

            isActive = !isActive;
        }
    }
}
