using UnityEngine;

public class ParticleForceUp : MonoBehaviour
{
    [SerializeField] float upwardForce = 50f; // Adjust this value to control the force applied
    [SerializeField] ParticleSystem _particleSystem;

    void OnParticleCollision(GameObject collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Apply an upward force to the object
                rb.AddForce(Vector3.up * upwardForce, ForceMode.VelocityChange);
            }
        }
    }
}
