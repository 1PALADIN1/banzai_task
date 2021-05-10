using Core.Data.Gun;
using Core.Data.Player;
using Core.Logic.Gun;
using Core.Views;
using UnityEngine;

namespace Core.Logic.Player
{
	public sealed class PlayerFactory : IPlayerFactory
	{
		private readonly IPlayerDatabase _playerDatabase;
		private readonly IGunDatabase _gunDatabase;
		private readonly IBulletSpawner _bulletSpawner;
		
		public PlayerFactory(IPlayerDatabase playerDatabase, IGunDatabase gunDatabase, IBulletSpawner bulletSpawner)
		{
			_playerDatabase = playerDatabase;
			_gunDatabase = gunDatabase;
			_bulletSpawner = bulletSpawner;
		}
		
		public PlayerView CreatePlayer()
		{
			var view = Object.Instantiate(_playerDatabase.ViewPrefab);
			view.Init(_playerDatabase.PlayerData, _gunDatabase.Guns, _bulletSpawner);
			return view;
		}
	}
}