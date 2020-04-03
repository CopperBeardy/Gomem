using GoMemory.Enums;
using GoMemory.Models;

namespace GoMemory.Interfaces
{
    public interface IResumeRepository
    {
        void UpdateGameResume(ResumeModel resumeModel);
        ResumeModel GetResumeModel(GameType gameType);

        void RemoveResumeModel(GameType gameType);
    }
}