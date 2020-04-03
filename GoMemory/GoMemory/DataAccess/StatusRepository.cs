using GoMemory.Enums;
using GoMemory.Interfaces;
using GoMemory.Models;
using SQLite;
using System;
using System.Collections.Generic;

namespace GoMemory.DataAccess
{


    public class StatusRepository : IStatusRepository
    {
        public readonly SQLiteConnection SyncConnection;
        public StatusRepository(string dbPath)
        {          
            SyncConnection = new SQLiteConnection(dbPath);
            SyncConnection.CreateTable<GameStatus>();
            SyncConnection.CreateTable<ResumeModel>();
        }

        public void UpdateGameStatus(GameStatus gameStatus)
        {
            try
            {
                GameStatus status = SyncConnection.Table<GameStatus>()
                    .FirstOrDefault(g => g.GameType == gameStatus.GameType &&
                    g.Difficulty == gameStatus.Difficulty);
                
                if (status == null || gameStatus.Level > status.Level)                   
                {
                     SyncConnection.InsertOrReplace(gameStatus);
                }     
            }
            catch (Exception ex)
            { 
                throw new Exception(nameof(UpdateGameStatus),ex);
            }
        }

        public List<GameStatus> GetGameStatus(GameType gameType) => 
            SyncConnection.Table<GameStatus>().Where(g => g.GameType == gameType).ToList();
    }
}
