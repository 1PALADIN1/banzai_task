using Core.Data.Gun;
using Core.Data.Player;
using Core.Views;
using UnityEngine;

namespace Core.Logic.Player
{
	public sealed class PlayerFactory : IPlayerFactory
	{
		private readonly IPlayerDatabase _playerDatabase;
		private readonly IGunDatabase _gunDatabase;
		
		public PlayerFactory(IPlayerDatabase playerDatabase, IGunDatabase gunDatabase)
		{
			_playerDatabase = playerDatabase;
			_gunDatabase = gunDatabase;
		}
		
		public PlayerView CreatePlayer()
		{
			var view = Object.Instantiate(_playerDatabase.ViewPrefab);
			view.Init(_playerDatabase.PlayerData, _gunDatabase.Guns);
			return view;
		}
	}
}