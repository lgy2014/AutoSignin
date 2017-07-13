using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace AutoSignin.People
{
    [Table("People")]
    public class Person : Entity
    {
        public string Name { get; set; }
    }
}
