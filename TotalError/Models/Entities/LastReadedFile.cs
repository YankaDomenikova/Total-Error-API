using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models.Base;

namespace Models.Entities
{
    public class LastReadedFile : BaseEntity
    {
        public DateTime LastReaded { get; set; }
    }
}
