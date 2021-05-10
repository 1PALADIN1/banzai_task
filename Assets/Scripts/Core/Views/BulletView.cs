using System;
using Core.Data.Bullet;
using Core.Logic;
using UnityEngine;

namespace Core.Views
{
    public class BulletView : MonoBehaviour
    {
        private const float MoveSpeed = 10f;
        private const float BulletLifeTime = 2f;

        [SerializeField] private BulletDamageComponent _damageComponent;
        [SerializeField] private MoveComponent _moveComponent;
        
        public event Action<BulletView> Destroyed;
        
        private float _currentLifeTime;
        private bool _isDead;

        public void Init(BulletData bulletData)
        {
            _currentLifeTime = BulletLifeTime;
            _isDead = false;

            transform.up = bulletData.MoveDirection.normalized;
            _damageComponent.Init(bulletData.HitSide, bulletData.Damage);
            _moveComponent.Init(MoveSpeed);
            _moveComponent.SetMoveDirection(bulletData.MoveDirection);
            gameObject.SetActive(true);
            
            _moveComponent.StartMoving();
        }

        private void Awake()
        {
            _damageComponent.DamageApplied += SetBulletDestroyed;
        }

        private void OnDestroy()
        {
            _damageComponent.DamageApplied -= SetBulletDestroyed;
        }

        private void Update()
        {
            if (_isDead)
                return;

            _currentLifeTime -= Time.deltaTime;
            
            if (_currentLifeTime <= 0f)
                SetBulletDestroyed();
        }

        private void OnDisable()
        {
            _moveComponent.StopMoving();
        }

        private void SetBulletDestroyed()
        {
            _isDead = true;
            gameObject.SetActive(false);
            Destroyed?.Invoke(this);
        }
    }
}