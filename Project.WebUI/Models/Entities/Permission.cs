using System;

namespace Project.WebUI.Models.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Profession { get; set; }
        public DateTime? Date { get; set; }
        public int Duration { get; set; }
        public string Reason { get; set; }
        public int DirectionId { get; set; }
        public int RegisterAdminId { get; set; }
        public int RegisterUserId { get; set; }
        public virtual Register Register { get; set; }
        public virtual Direction Direction { get; set; }


    }
}
