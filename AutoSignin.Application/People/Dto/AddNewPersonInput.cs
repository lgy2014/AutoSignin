using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace AutoSignin.People.Dto
{
    public class AddNewPersonInput : IInputDto
    {
        [Required]
        public string Name { get; set; }
    }
}