using CicekSepetiTech.Case.Domain.DbEntity.Base;
using System;

namespace CicekSepetiTech.Case.Data.DbEntity
{
    public class Customer : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}