using System.Threading.Tasks;
using Abp.Application.Services;
using AutoSignin.People.Dto;

namespace AutoSignin.People
{
    public interface IPersonAppService : IApplicationService
    {
        Task<GetPeopleOutput> GetAllPeopleAsync();

        Task AddNewPerson(AddNewPersonInput input);
    }
}