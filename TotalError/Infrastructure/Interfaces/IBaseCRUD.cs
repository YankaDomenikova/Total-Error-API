using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Infrastructure.DtoModels;

using Models.Entities;

namespace Infrastructure.Interfaces
{
    public interface IBaseCRUD
    {
        public void ReadFile(string dir);
        public void SaveToDB(List<FileTransferModel> records, string fileName);
    }
}
