using UnityEngine;

namespace Core.Data.Bullet
{
    public readonly struct BulletData
    {
        public int Damage { get; }
        public LayerMask HitSide { get; }
        public Vector2 MoveDirection { get; }

        public BulletData(int damage, LayerMask hitSide, Vector2 moveDirection)
        {
            Damage = damage;
            HitSide = hitSide;
            MoveDirection = moveDirection;
        }
    }
}