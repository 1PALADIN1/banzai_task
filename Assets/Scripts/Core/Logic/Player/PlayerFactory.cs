using Core.Data.Player;
using Core.Views;
using UnityEngine;

namespace Core.Logic.Player
{
	public sealed class PlayerFactory : IPlayerFactory
	{
		private readonly IPlayerDatabase _playerDatabase;
		
		public PlayerFactory(IPlayerDatabase playerDatabase)
		{
			_playerDatabase = playerDatabase;
		}
		
		public PlayerView CreatePlayer()
		{
			var view = Object.Instantiate(_playerDatabase.ViewPrefab);
			view.Init(_playerDatabase.PlayerData);
			return view;
		}
	}
}