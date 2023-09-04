using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _livesText;
    [SerializeField] TMP_Text _fuelText;

    Slider _fuelSlider;

    Player _player;

    public void Bind(Player player)
    {
        _player = player;
        _fuelSlider = FindObjectOfType<Slider>();
        _player.EnergyChanged += UpdateEnergy;
        UpdateEnergy();
        _player.LivesChanged += UpdateLives;
        UpdateLives();
    }

    void UpdateLives()
    {
        _livesText.SetText("Lives: " + _player.Lives.ToString());
    }

    void UpdateEnergy()
    {
        _scoreText.SetText("Energy: " + _player.Energy.ToString());
    }

    void Update()
    {
        if (_player)
        {
            _fuelSlider.value = _player.Fuel;
        }
    }
}
