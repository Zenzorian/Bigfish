using ScriptableObjects;
using Scripts.Logic;

namespace Scripts.Services
{
    public interface IPlayerBallPoolService
    {
        PlayerBall GetPlayerBall(PlayerBallConfig config);
        void InitializePool();
        void ReturnBall(PlayerBall playerBall);
    }
}