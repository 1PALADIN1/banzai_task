using System;
using Core.Views;
using UnityEngine;

namespace Core.Logic
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private HpUiView _hpUiView;
        
        private int _maxHp;
        private int _currentHp;
        private float _defence;
        
        public event Action<int> HealthChanged;

        public void Init(int maxHp, float defence)
        {
            _maxHp = maxHp;
            _currentHp = _maxHp;
            _defence = defence;
            UpdateHpBar();
        }

        public void ApplyDamage(int damage)
        {
            if (_currentHp <= 0)
                return;
            
            _currentHp -= (int) (damage * (1f - _defence));
            _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);
            
            UpdateHpBar();
            HealthChanged?.Invoke(_currentHp);
        }

        private void UpdateHpBar()
        {
            _hpUiView.SetHp(_currentHp, _maxHp);
        }
    }
}