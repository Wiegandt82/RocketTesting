using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource), typeof(Player))] 
public class CollisionHandler : MonoBehaviour
{
    [Header("Level Load Delay")]
    [SerializeField] float _levelLoadDelay = 3f;

    [Header("Audio Clips")]
    [SerializeField] AudioClip _success;
    [SerializeField] AudioClip _crash;

    [Header("Particle Systems")]
    [SerializeField] ParticleSystem _crashParticle;
    [SerializeField] ParticleSystem _successParticle;

    AudioSource _audioSource;
    Player _player;
    bool isTransitioning = false;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _player = GetComponent<Player>();
    }


    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
            break;

            case "Fuel":
                Debug.Log("Fuel collected");
            break;

            case "Point":
                Debug.Log("Point collected");
            break;
            
            case "Life":
                Debug.Log("Life collected");
            break;

            case "Finish":
                StartSuccessSequence();
            break;

            default:
                StartCrashSequence();
            break;
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (isTransitioning) return;

        if (other.CompareTag("Fire"))
            StartCrashSequence();
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        PlayAudioAndParticles(_success, _successParticle);
        DisablePlayerAndLoadNextLevel();
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        PlayAudioAndParticles(_crash, _crashParticle);
        DisablePlayerAndReloadLevel();
    }

    

    void PlayAudioAndParticles(AudioClip clip, ParticleSystem particleSystem)
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(clip);
        particleSystem.Play();
    }

    void DisablePlayerAndLoadNextLevel()
    {
        _player.enabled = false;
        Invoke("LoadNextLevel", _levelLoadDelay);
    }

    void DisablePlayerAndReloadLevel()
    {
        _player.enabled = false;
        _player.DecrementLives();

        if(_player.Lives <= 0)
        {
            SceneManager.LoadScene(0);
            return;
        }

        Invoke("ReloadLevel", _levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;

        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
