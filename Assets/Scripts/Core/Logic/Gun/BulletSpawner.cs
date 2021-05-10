using System.Collections.Generic;
using Core.Data.Bullet;
using Core.Views;
using UnityEngine;
using Zenject;

namespace Core.Logic.Gun
{
    public class BulletSpawner : MonoBehaviour, IBulletSpawner
    {
        private readonly HashSet<BulletView> _activeBulletViews = new HashSet<BulletView>();
        
        private IBulletFactory _bulletFactory;

        [Inject]
        public void Init(IBulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public void SpawnBullet(BulletData bulletData, Vector2 spawnPosition)
        {
            var bulletView = _bulletFactory.CreateBullet(bulletData, spawnPosition);
            bulletView.Destroyed += OnBulletDestroyed;
            _activeBulletViews.Add(bulletView);
        }
        
        private void OnDestroy()
        {
            foreach (var bulletView in _activeBulletViews)
                bulletView.Destroyed -= OnBulletDestroyed;
			
            _activeBulletViews.Clear();
        }

        private void OnBulletDestroyed(BulletView bulletView)
        {
            bulletView.Destroyed -= OnBulletDestroyed;
            _activeBulletViews.Remove(bulletView);
            _bulletFactory.Release(bulletView);
        }
    }
}