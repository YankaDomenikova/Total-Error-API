using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Interfaces
{
    public interface IBaseCRUD
    {
        public void ReadFile(string dir);
        //public void SaveToDB(List<FileTransferModel> records, string fileName);
    }
}
