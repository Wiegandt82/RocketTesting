using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField] PlayerPanel _playerPanel;

    public void Bind(Player player)
    {
        _playerPanel.Bind(player);
    }
}
