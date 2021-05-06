using Core.Logic.Monster;
using Core.Logic.Player;
using Zenject;

namespace Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMonsterFactory>().To<MonsterFactory>().AsSingle().NonLazy();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle().NonLazy();
        }
    }
}