using Core.Logic.Monster;
using Core.Logic.Player;
using Core.Logic.Scene;
using Core.Views;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraView _cameraView;
        
        public override void InstallBindings()
        {
            Container.Bind<IMonsterFactory>().To<MonsterFactory>().AsSingle().NonLazy();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle().NonLazy();
            Container.Bind<CameraView>().FromInstance(_cameraView).AsSingle().NonLazy();
            Container.Bind<ISceneController>().To<SceneController>().AsSingle().NonLazy();
        }
    }
}