using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet.Entities
{
    public class UserOnProject
    {
        [ForeignKey("User")]
        [Key]
        [Column(Order = 0)]
        public int UserId { get; set; }
        
        public User User { get; set; }

        [ForeignKey("Project")]
        [Key]
        [Column(Order = 1)]
        public int ProjectId { get; set; }
       
        public Project Project { get; set; }

        public UserOnProject()
        {

        }

        public UserOnProject(int UserId, int ProjectId)
        {
            this.UserId = UserId;
            this.ProjectId = ProjectId;
        }
    }
}
