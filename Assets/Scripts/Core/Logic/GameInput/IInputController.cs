using System;
using UnityEngine;

namespace Core.Logic.GameInput
{
    public interface IInputController
    {
        event Action<Vector2> MoveAxisStateChanged;
        event Action<Vector2> RotateAxisStateChanged;
        event Action<int> GunChanged;
    }
}