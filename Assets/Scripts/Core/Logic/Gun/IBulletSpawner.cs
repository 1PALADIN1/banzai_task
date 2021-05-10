using Core.Data.Bullet;
using UnityEngine;

namespace Core.Logic.Gun
{
    public interface IBulletSpawner
    {
        void SpawnBullet(BulletData bulletData, Vector2 spawnPosition);
    }
}