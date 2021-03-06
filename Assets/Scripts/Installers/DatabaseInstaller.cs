using Core.Data.Bullet;
using Core.Data.Gun;
using Core.Data.Monster;
using Core.Data.Player;
using UnityEngine;
using Zenject;

namespace Installers
{
	public sealed class DatabaseInstaller : MonoInstaller
	{
		[SerializeField] private PlayerDatabase _playerDatabase;
		[SerializeField] private GunDatabase _gunDatabase;
		[SerializeField] private MonsterDatabase _monsterDatabase;
		[SerializeField] private BulletDatabase _bulletDatabase;
		
		public override void InstallBindings()
		{
			Container.Bind<IPlayerDatabase>().FromInstance(_playerDatabase).AsSingle().NonLazy();
			Container.Bind<IGunDatabase>().FromInstance(_gunDatabase).AsSingle().NonLazy();
			Container.Bind<IMonsterDatabase>().FromInstance(_monsterDatabase).AsSingle().NonLazy();
			Container.Bind<IBulletDatabase>().FromInstance(_bulletDatabase).AsSingle().NonLazy();
		}
	}
}