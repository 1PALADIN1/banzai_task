using System;
using UnityEngine;

namespace Core.Logic
{
    public class HealthComponent : MonoBehaviour
    {
        private int _maxHp;
        private int _currentHp;
        private float _defence;
        
        public event Action<int> HealthChanged;

        public void Init(int maxHp, float defence)
        {
            _maxHp = maxHp;
            _currentHp = _maxHp;
            _defence = defence;
        }

        public void ApplyDamage(int damage)
        {
            if (_currentHp <= 0)
                return;
            
            _currentHp -= (int) (damage * (1f - _defence));
            _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);
            Debug.Log($"Damage {damage} applied. Hp: {_currentHp} / {_maxHp}"); //TODO
            
            HealthChanged?.Invoke(_currentHp);
        }
    }
}