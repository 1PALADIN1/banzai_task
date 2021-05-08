using System;
using UnityEngine;

namespace Core.Logic.Scene
{
    public interface ISceneController
    {
        event Action GameStarted;
        event Action GameFinished;
        
        bool IsGameStarted { get; }
        bool IsGameFinished { get; }
        
        void StartGame();
        void FinishGame();
        
        Vector2 GetRandomPointOutOfScene();
    }
}