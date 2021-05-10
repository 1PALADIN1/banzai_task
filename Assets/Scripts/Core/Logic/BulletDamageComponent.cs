using System;
using UnityEngine;

namespace Core.Logic
{
    public class BulletDamageComponent : MonoBehaviour
    {
        private LayerMask _hitMask;
        private int _damage;

        public event Action DamageApplied;
        
        public void Init(LayerMask hitMask, int damage)
        {
            _hitMask = hitMask;
            _damage = damage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((other.gameObject.layer & (1 << _hitMask)) == 0)
                return;
            
            if (!other.TryGetComponent<HealthComponent>(out var health))
                return;
            
            health.ApplyDamage(_damage);
            DamageApplied?.Invoke();
        }
    }
}