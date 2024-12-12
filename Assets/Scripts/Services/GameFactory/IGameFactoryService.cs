using Scripts.Logic;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Services
{
    public interface IGameFactoryService
    {
        void CreateGameFild();
        void SpawnPlayerBall(BallType ballType);

        UnityEvent<float, BallType> OnWin { get; set; }

        Vector2[] GetGridCoordinates();
    }
}