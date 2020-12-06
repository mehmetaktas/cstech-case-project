using CicekSepetiTech.Case.Domain.DbEntity.Base;
using System;

namespace CicekSepetiTech.Case.Data.DbEntity
{
    public class Product : BaseEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal PriceInclTax { get; set; }
        public int SalableQuantity { get; set; }
        public int ProductAvailabilityId { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual ProductAvailability ProductAvailability { get; set; }
    }
}