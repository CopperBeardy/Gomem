using GoMemory.Enums;
using GoMemory.Models;

namespace GoMemory.Helpers
{
    public static class ResumeHelper
    {
        public static ResumeModel CheckResume(GameType gameType)
        {
            return App.ResumeRepository.GetResumeModel(gameType);
        }

        public static void SetResume(ResumeModel resumeModel)
        {

            App.ResumeRepository.UpdateGameResume(resumeModel);
        }

        public static void RemoveResume(GameType gameType)
        {
            App.ResumeRepository.RemoveResumeModel(gameType);
        }
    }
}
