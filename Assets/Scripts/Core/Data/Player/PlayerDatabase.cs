using Core.Views;
using UnityEngine;

namespace Core.Data.Player
{
    public interface IPlayerDatabase
    {
        PlayerView ViewPrefab { get; }
        IPlayerData PlayerData { get; }
    }
    
    [CreateAssetMenu(fileName = "PlayerDatabase", menuName = "Game/Data/PlayerDatabase")]
    public sealed class PlayerDatabase : ScriptableObject, IPlayerDatabase
    {
        [SerializeField] private PlayerView _viewPrefab;
        [SerializeField] private PlayerData _playerData;

        public PlayerView ViewPrefab => _viewPrefab;
        public IPlayerData PlayerData => _playerData;
    }
}
