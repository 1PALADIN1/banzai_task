using Core.Views;

namespace Core.Logic.Player
{
	public interface IPlayerFactory
	{
		PlayerView CreatePlayer();
	}
}