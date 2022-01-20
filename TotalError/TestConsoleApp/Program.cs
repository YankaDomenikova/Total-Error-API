using Data;

using Infrastructure.Constants;

using Services.Implementations;

namespace TestConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BaseCRUDService baseCRUD = new BaseCRUDService(new ApplicationDbContext());
            baseCRUD.ReadFile(DirectoryConstant.Directory);
        }
    }
}
