using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CsvHelper;

using Data.AppDbContext;
using Data.Models.Entities;
using Data.Services.TransferModel;
using Data.Utilities.Interfaces;

namespace Data.Services.Implementations
{
    public class BaseCRUDService : IBaseCRUD
    {
        public BaseCRUDService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        public void ReadFile(string dir)
        {
            string[] files = System.IO.Directory.GetFiles(dir);

            List<FileTransferModel> records = new List<FileTransferModel>();

            var lastReadedDate = DbContext.LastReadedFiles.OrderByDescending(x => x.LastReaded);

            foreach (string currentFile in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(currentFile);

                if (lastReadedDate.Count() > 0)
                {
                    if (lastReadedDate.First().LastReaded < DateTime.Parse(fileName))
                    {
                        using (var reader = new StreamReader(currentFile))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            records = csv.GetRecords<FileTransferModel>().ToList();
                        }
                        DbContext.LastReadedFiles.Add(new LastReadedFile() { LastReaded = DateTime.Parse(fileName) });
                        //SaveToDB(records, fileName);
                    }
                    DbContext.SaveChanges();
                }
                else
                {
                    using (var reader = new StreamReader(currentFile))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        records = csv.GetRecords<FileTransferModel>().ToList();
                    }
                    DbContext.LastReadedFiles.Add(new LastReadedFile() { LastReaded = DateTime.Parse(fileName) });
                    //SaveToDB(records, fileName);
                    DbContext.SaveChanges();
                }
            }

        }
    }
}
