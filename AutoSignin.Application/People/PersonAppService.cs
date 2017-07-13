using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using AutoSignin.People.Dto;
using AutoMapper;
using Castle.Core.Logging;

namespace AutoSignin.People
{
    public class PersonAppService : AutoSigninAppServiceBase, IPersonAppService
    {
        public ILogger Logger { get; set; }

        private readonly IRepository<Person> _personRepository;

        public PersonAppService(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
            Logger = NullLogger.Instance;
        }

        public async Task<GetPeopleOutput> GetAllPeopleAsync()
        {
            Logger.Debug("Getting all people");

            return new GetPeopleOutput
                   {
                       People = Mapper.Map<List<PersonDto>>(await _personRepository.GetAllListAsync())
                   };
        }

        public async Task AddNewPerson(AddNewPersonInput input)
        {
            Logger.Debug("Adding a new person: " + input.Name);
            await _personRepository.InsertAsync(new Person { Name = input.Name });
        }
    }
}
