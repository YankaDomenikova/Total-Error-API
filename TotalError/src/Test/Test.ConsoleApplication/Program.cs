using System;

using Data.AppDbContext;
using Data.Services.Implementations;
using Data.Utilities.Constants;

namespace Test.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseCRUDService baseCRUD = new BaseCRUDService(new ApplicationDbContext());
            baseCRUD.ReadFile(DirectoryConstant.Directory);
        }
    }
}
