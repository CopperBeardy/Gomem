using GoMemory.Enums;
using GoMemory.Interfaces;
using GoMemory.Models;
using SQLite;
using System;

namespace GoMemory.DataAccess
{
    public class ResumeRepository : IResumeRepository
    {
        public SQLiteConnection Conn;

        public ResumeRepository(string dbPath)
        {
            Conn = new SQLiteConnection(dbPath);
            Conn.CreateTable<ResumeModel>();
        }

        public void UpdateGameResume(ResumeModel resumeModel)
        {
            try
            {
                if (resumeModel.Level != 0)
                    Conn.InsertOrReplace(resumeModel);
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(UpdateGameResume), ex);
            }

        }

        public void RemoveResumeModel(GameType gameType)
        {
            try
            {
                Conn.Table<ResumeModel>().Delete(g => g.GameType == gameType);
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(RemoveResumeModel), ex);
            }
        }

        public ResumeModel GetResumeModel(GameType gameType)
        {
            try
            {
                return Conn.Table<ResumeModel>().FirstOrDefault(g => g.GameType == gameType);

            }
            catch (Exception ex)
            {

                throw new Exception(nameof(GetResumeModel), ex); ;
            }
        }
    }
}