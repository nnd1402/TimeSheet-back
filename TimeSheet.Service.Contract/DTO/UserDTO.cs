namespace TimeSheet.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public double HoursPerWeek { get; set; }
        public bool Status { get; set; }
        public int RoleId { get; set; }
    }
}
