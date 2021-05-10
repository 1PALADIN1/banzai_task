using Core.Views;
using UnityEngine;

namespace Core.Data.Bullet
{
    public interface IBulletDatabase
    {
        BulletView ViewPrefab { get; }
    }
    
    [CreateAssetMenu(fileName = "BulletDatabase", menuName = "Game/Data/BulletDatabase")]
    public sealed class BulletDatabase : ScriptableObject, IBulletDatabase
    {
        [SerializeField] private BulletView _viewPrefab;

        public BulletView ViewPrefab => _viewPrefab;
    }
}