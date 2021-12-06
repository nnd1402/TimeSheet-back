using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet.Entities
{
    public class TeamLeader
    {
        [Key]
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public TeamLeader()
        {

        }

        [ForeignKey("UserId, ProjectId")]
        public UserOnProject UserOnProject { get; set; }
        public TeamLeader(int UserId, int ProjectId)
        {
            this.UserId = UserId;
            this.ProjectId = ProjectId;
        }
    }
}
