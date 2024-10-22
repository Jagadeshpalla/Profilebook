namespace VBOOK2.Backend.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public ICollection<User> GroupMembers { get; set; }
    }
}
