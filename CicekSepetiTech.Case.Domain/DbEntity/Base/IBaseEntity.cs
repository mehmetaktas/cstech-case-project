using System.ComponentModel.DataAnnotations;

namespace CicekSepetiTech.Case.Domain.DbEntity.Base
{
    public interface IBaseEntity<T>
    {
        [Key]
        T Id { get; set; }
    }
}