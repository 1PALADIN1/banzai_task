using System.Collections;
using UnityEngine;

namespace Core.Logic
{
    public class DamageComponent : MonoBehaviour
    {
        [Header("Damage")]
        [SerializeField] private float _damageScanRadius = 0.5f;
        [SerializeField] private float _damageCooldownTime = 1f;
        [SerializeField] private LayerMask _hitMask;
        
        private readonly RaycastHit2D[] _raycastResults = new RaycastHit2D[10];
        
        private Coroutine _restoreDamageAbilityCoroutine;
        private bool _canApplyDamage;
        private int _damage;

        public void Init(int damage)
        {
            _damage = damage;
            _canApplyDamage = true;
        }

        private void OnDisable()
        {
            if (_restoreDamageAbilityCoroutine != null)
                StopCoroutine(_restoreDamageAbilityCoroutine);
        }

        private void FixedUpdate()
        {
            if (!_canApplyDamage)
                return;
			
            var castSize = Physics2D.CircleCastNonAlloc(transform.position, _damageScanRadius, 
                Vector2.zero, _raycastResults, 0f, _hitMask);
			
            if (castSize == 0)
                return;

            _canApplyDamage = false;
			
            for (var i = 0; i < castSize; i++)
            {
                var hitObject = _raycastResults[i].collider;
                if (!hitObject.TryGetComponent<HealthComponent>(out var hitHealth))
                    continue;
				
                hitHealth.ApplyDamage(_damage);
            }

            _restoreDamageAbilityCoroutine = StartCoroutine(RestoreDamageAbility());
        }

        private IEnumerator RestoreDamageAbility()
        {
            yield return new WaitForSeconds(_damageCooldownTime);
            _canApplyDamage = true;
        }
    }
}