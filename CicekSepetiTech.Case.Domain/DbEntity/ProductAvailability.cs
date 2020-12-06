using CicekSepetiTech.Case.Domain.DbEntity.Base;
using System;

namespace CicekSepetiTech.Case.Data.DbEntity
{
    public class ProductAvailability : BaseEntity<int>
    {
        public string Name { get; set; }
        public bool Display { get; set; }
        public bool Salable { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateDate { get; set; }
    }
}