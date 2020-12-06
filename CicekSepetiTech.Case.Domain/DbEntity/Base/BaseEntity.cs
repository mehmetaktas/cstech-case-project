using System.ComponentModel.DataAnnotations;

namespace CicekSepetiTech.Case.Domain.DbEntity.Base
{
    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        [Key]
        public virtual T Id { get; set; }
    }
}