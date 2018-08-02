using CAPExamples.EntityFramework.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CAPExamples.EntityFramework.Abstractions.Services
{
    public interface IUserSubcriberServices
    {
        Task<User> CreateAsync(User user);
    }
}
