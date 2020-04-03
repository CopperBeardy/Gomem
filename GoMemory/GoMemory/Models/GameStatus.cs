using GoMemory.Enums;
using SQLite;

namespace GoMemory.Models
{
    [Table("GameStatus")]
    public class GameStatus
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public GameType GameType { get; set; }
        public int Level { get; set; }
        public Difficulty Difficulty { get; set; }


    }
}
