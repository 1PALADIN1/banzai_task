using Core.Data.Bullet;
using Core.Views;
using UnityEngine;

namespace Core.Logic.Gun
{
    public interface IBulletFactory
    {
        BulletView CreateBullet(BulletData bulletData, Vector2 bulletSpawnPoint);
        void Release(BulletView bulletData);
    }
}