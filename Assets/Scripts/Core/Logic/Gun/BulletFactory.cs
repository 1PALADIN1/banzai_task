using Core.Data.Bullet;
using Core.Utils;
using Core.Views;
using UnityEngine;

namespace Core.Logic.Gun
{
    public class BulletFactory : IBulletFactory
    {
        private readonly IBulletDatabase _bulletDatabase;
        private readonly GameObjectPool<BulletView> _bulletPool;

        public BulletFactory(IBulletDatabase bulletDatabase)
        {
            _bulletDatabase = bulletDatabase;
            _bulletPool = new GameObjectPool<BulletView>();
        }
        
        public BulletView CreateBullet(BulletData bulletData, Vector2 bulletSpawnPoint)
        {
            if (!_bulletPool.TryGetObject(out var bulletView))
                bulletView = Object.Instantiate(_bulletDatabase.ViewPrefab);

            bulletView.transform.position = bulletSpawnPoint;
            bulletView.Init(bulletData);
            return bulletView;
        }

        public void Release(BulletView bulletView)
        {
            _bulletPool.Release(bulletView);
        }
    }
}