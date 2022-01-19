using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Base
{
    public interface IEntity : IBaseIdentity, ICreatedInfo, IDeletedInfo
    {
    }
}
