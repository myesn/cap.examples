using CAPExamples.EntityFramework.Abstractions;
using CAPExamples.EntityFramework.Abstractions.DbContexts;
using CAPExamples.EntityFramework.Abstractions.Entities;
using CAPExamples.EntityFramework.Abstractions.Services;
using DotNetCore.CAP;
using DotNetCore.CAP.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPExamples.EntityFramework.Subcribe.Host.SubcribeServices
{
    public class UserSubcriberServices : IUserSubcriberServices, ICapSubscribe
    {
        public UserSubcriberServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private readonly AppDbContext _dbContext;


        [CapSubscribe(TopicNames.UserCreate)]
        public async Task<User> CreateAsync(User user)
        {
            _dbContext.Add(user);

            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}
