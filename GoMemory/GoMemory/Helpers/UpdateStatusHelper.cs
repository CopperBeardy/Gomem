using GoMemory.Models;
using System;

namespace GoMemory.Helpers
{
    public static class UpdateStatusHelper
    {
        public static void UpdateStatus(GameStatus gameStatus)
        {
            if (gameStatus.Level != 0)
            {
                App.StatusRepository.UpdateGameStatus(gameStatus);
            }
        }
    }
}
