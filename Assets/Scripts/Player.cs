using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Tuning
    [SerializeField] float _mainThrust = 100f;
    [SerializeField] float _rotationThrust = 1f;
    [SerializeField] float _fuelUsage = 10f;

    [SerializeField] AudioClip _mainEngineSfx;
    [SerializeField] AudioClip _pointSfx;
    [SerializeField] AudioClip _lifeCollectedSfx;
    [SerializeField] AudioClip _fuelCollectedSfx;

    [SerializeField] ParticleSystem _mainEngineParticle;
    [SerializeField] ParticleSystem _leftEngineParticles;
    [SerializeField] ParticleSystem _rightEngineParticles;

    //CACHE
    Rigidbody _rb;
    PlayerInput _playerInput;

    [SerializeField] AudioSource _mainEngineAudioSource;
    [SerializeField] AudioSource _pointCollectedAudioSource;
    [SerializeField] AudioSource _lifeCollectedAudioSource;
    [SerializeField] AudioSource _fuelCollectedAudioSource;

    PlayerData _playerData; //will hold player data for collectables
    bool _isThrusting;
    float _rotationInput;

    public event Action EnergyChanged;
    public event Action LivesChanged;

    //Properties
    public int Energy { get => _playerData.Energy; private set => _playerData.Energy = value; } //get and set points from player data class
    public float Fuel { get => _playerData.Fuel; private set => _playerData.Fuel = value; } //get and set fuel from player data class
    public int Lives => _playerData.Lives;  //get lives from player data class

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Move"].performed += ctx => HandleMove(ctx.ReadValue<Vector2>());
        _playerInput.actions["Move"].canceled += ctx => HandleMove(Vector2.zero);
        _playerInput.actions["Thrust"].performed += ctx => _isThrusting = true;
        _playerInput.actions["Thrust"].canceled += ctx => _isThrusting = false;

        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
        _mainEngineAudioSource = audioSources[0]; 
        _pointCollectedAudioSource = audioSources[1]; 
        _lifeCollectedAudioSource = audioSources[2]; 
        _fuelCollectedAudioSource = audioSources[3];

        

        // Get the PlayerData instance from GameManager
        _playerData = GameManager.Instance.playerData;
    }

    void HandleMove(Vector2 value)
    {
        _rotationInput = -value.x;

        if (value.x > 0)
        {
            if (_leftEngineParticles != null && !_leftEngineParticles.isPlaying)
                _leftEngineParticles.Play();
        }
        else if (value.x < 0)
        {
            
            if (_rightEngineParticles != null && !_rightEngineParticles.isPlaying)
                _rightEngineParticles.Play();
        }
        else
        {
            if (_rightEngineParticles != null)
                _rightEngineParticles.Stop();
            if (_leftEngineParticles != null)
                _leftEngineParticles.Stop();
        }
    }

    void Start()
    {
        FindObjectOfType<PlayerCanvas>().Bind(this);
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (_isThrusting)
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StopThrusting()
    {
        _mainEngineAudioSource.Stop();
        _mainEngineParticle.Stop();
    }

    void StartThrusting()
    {
        _rb.AddRelativeForce(Vector3.up * _mainThrust * Time.fixedDeltaTime);

        //Using down fuel per every second
        _playerData.Fuel -= _fuelUsage * Time.fixedDeltaTime;

        if (!_mainEngineAudioSource.isPlaying)
            _mainEngineAudioSource.PlayOneShot(_mainEngineSfx);

        if (!_mainEngineParticle.isPlaying)
            _mainEngineParticle.Play();
    }

    void ProcessRotation()
    {
        ApplyRotation(_rotationInput * _rotationThrust);
    }

    void ApplyRotation(float rotationThisFrame)
    {
        _rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rb.freezeRotation = false;
    }

    public void FuelCollected()
    {
        int maxFuel = 200;
        _fuelCollectedAudioSource.PlayOneShot(_fuelCollectedSfx);
        _playerData.Fuel += 40;

        if (_playerData.Fuel > maxFuel)
            _playerData.Fuel = maxFuel;
    }

    public void LifeCollected()
    {
        _lifeCollectedAudioSource.PlayOneShot(_lifeCollectedSfx);
        _playerData.Lives += 1;
        LivesChanged();
    }
    
    public void EnergyCollected()
    {
        _pointCollectedAudioSource.PlayOneShot(_pointSfx);
        _playerData.Energy += 10;
        EnergyChanged?.Invoke();
    }

    public void BindPlayerData(PlayerData playerData)
    {
        _playerData = playerData;
    }

    public void DecrementLives()
    {
        _playerData.Lives--;
    }
}
