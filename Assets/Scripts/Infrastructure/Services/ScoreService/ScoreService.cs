using System;
using Infrastructure.Services.Data;
using UnityEngine;

namespace Infrastructure.Services.ScoreService
{
    public class ScoreService : IScoreService
    {
        private readonly IDataService _dataService;
        public int CurrentScore { get; private set; }

        public event Action IsWin;
        public event Action<int> ScoreIsUpdate;

        public ScoreService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void ResetPoints()
        {
            CurrentScore = 0;
            ScoreIsUpdate?.Invoke(CurrentScore);
        }

        public void AddScorePoint()
        {
            CurrentScore++;
            CheckWin();
            ScoreIsUpdate?.Invoke(CurrentScore);
        }

        private void CheckWin()
        {
            if (CurrentScore >= _dataService.StaticData.WinPointsNumber)
            {
                IsWin?.Invoke();
            }
        }
    }
}