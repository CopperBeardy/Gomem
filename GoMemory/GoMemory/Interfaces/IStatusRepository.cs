using GoMemory.Enums;
using GoMemory.Models;
using System.Collections.Generic;

namespace GoMemory.Interfaces
{
    public interface IStatusRepository
    {
        void UpdateGameStatus(GameStatus gameStatus);
        List<GameStatus> GetGameStatus(GameType gameType);
    }
}