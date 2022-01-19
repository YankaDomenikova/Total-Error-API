using System;

using Data;

using Infrastructure.Constants;

using Services.Implementations;

namespace TestConsoleApp
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
