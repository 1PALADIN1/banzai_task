using System.Collections;
using Core.Data.Bullet;
using Core.Data.Gun;
using Core.Logic.Gun;
using UnityEngine;

namespace Core.Views
{
	public class GunView : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private Transform _bulletSpawnPoint;
		
		private IGunData _gunData;
		private LayerMask _hitSide;

		private IBulletSpawner _bulletSpawner;
		private Coroutine _restoreFireAbilityCoroutine;
		private bool _canFire;

		public void Init(IBulletSpawner bulletSpawner, LayerMask hitSide)
		{
			_bulletSpawner = bulletSpawner;
			_hitSide = hitSide;
		}
		
		public void SetGun(IGunData gunData)
		{
			_gunData = gunData;
			_spriteRenderer.sprite = _gunData.GunSprite;
			
			if (_restoreFireAbilityCoroutine != null)
				StopCoroutine(_restoreFireAbilityCoroutine);
			_canFire = true;
		}

		public void Fire()
		{
			if (!_canFire)
				return;

			_canFire = false;

			var bulletData = new BulletData(_gunData.Damage, _hitSide, transform.up);
			_bulletSpawner.SpawnBullet(bulletData, _bulletSpawnPoint.position);
			_restoreFireAbilityCoroutine = StartCoroutine(RestoreFireAbility(_gunData.CooldownTime));
		}

		private IEnumerator RestoreFireAbility(float restoreTime)
		{
			yield return new WaitForSeconds(restoreTime);
			_canFire = true;
		}
		
		private void OnDisable()
		{
			if (_restoreFireAbilityCoroutine != null)
				StopCoroutine(_restoreFireAbilityCoroutine);
		}
	}
}