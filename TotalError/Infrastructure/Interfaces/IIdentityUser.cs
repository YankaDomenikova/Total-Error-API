using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Infrastructure.DtoModels;

namespace Infrastructure.Interfaces
{
    public interface IIdentityUser
    {
        public Task<string> Register(UserVM model);
        public Task<string> Login(UserVM model);
    }
}
