using Core.Data.Gun;
using Core.Data.Player;
using Core.Logic.Gun;
using Core.Logic.Scene;
using Core.Views;
using UnityEngine;

namespace Core.Logic.Player
{
	public sealed class PlayerFactory : IPlayerFactory
	{
		private readonly IPlayerDatabase _playerDatabase;
		private readonly IGunDatabase _gunDatabase;
		private readonly IBulletSpawner _bulletSpawner;
		private readonly SceneBounds _sceneBounds;
		
		public PlayerFactory(IPlayerDatabase playerDatabase, IGunDatabase gunDatabase, 
			IBulletSpawner bulletSpawner, ISceneController sceneController)
		{
			_playerDatabase = playerDatabase;
			_gunDatabase = gunDatabase;
			_bulletSpawner = bulletSpawner;
			_sceneBounds = sceneController.GetSceneBounds();
		}
		
		public PlayerView CreatePlayer()
		{
			var view = Object.Instantiate(_playerDatabase.ViewPrefab);
			view.Init(_playerDatabase.PlayerData, _gunDatabase.Guns, _bulletSpawner, _sceneBounds);
			return view;
		}
	}
}