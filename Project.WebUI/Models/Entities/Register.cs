using System;

namespace Project.WebUI.Models.Entities
{
    public class Register
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FinCode { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool RegisterActive { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        //public int UserStatusId { get; set; }
        //public virtual Status Status { get; set; }
        //public int ProfessionId { get; set; }
        //public virtual Profession Profession { get; set; }  
    }
}
