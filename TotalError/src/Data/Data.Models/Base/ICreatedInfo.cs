using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Base
{
    public interface ICreatedInfo
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
