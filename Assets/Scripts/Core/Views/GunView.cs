using Core.Data.Gun;
using Core.Utils;
using UnityEngine;

namespace Core.Views
{
	public sealed class GunView : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private Transform _bulletSpawnPoint;

		private readonly GameObjectPool<BulletView> _bullets = new GameObjectPool<BulletView>();
		
		private IGunData _gunData;

		public void Init(IGunData gunData)
		{
			_gunData = gunData;
			_spriteRenderer.sprite = _gunData.GunSprite;
		}

		public void Fire()
		{
			//TODO
		}
	}
}