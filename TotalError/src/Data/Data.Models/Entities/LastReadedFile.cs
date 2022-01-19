using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data.Models.Base;

namespace Data.Models.Entities
{
    public class LastReadedFile : BaseEntity
    {
        public DateTime LastReaded { get; set; }
    }
}
