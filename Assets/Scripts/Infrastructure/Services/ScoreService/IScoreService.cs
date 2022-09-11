using System;

namespace Infrastructure.Services.ScoreService
{
    public interface IScoreService : IService
    {
        event Action IsWin;
        event Action<int> ScoreIsUpdate;
        int CurrentScore { get; }
        void AddScorePoint();
        void ResetPoints();
    }
}