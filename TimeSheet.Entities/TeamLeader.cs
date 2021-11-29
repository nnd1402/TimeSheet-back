using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet.Entities
{
    public class TeamLeader
    {
        [Key]
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId, ProjectId")]
        public UserOnProject UserOnProject { get; set; }
    }
}
