using System.Threading.Tasks;

using Data;

using Infrastructure.Constants;

using Quartz;

using Services.Implementations;

namespace API.Scheduler
{
    public class SchedulerReader : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            BaseCRUDService baseCRUD = new BaseCRUDService(new ApplicationDbContext());
            await baseCRUD.ReadFile(DirectoryConstant.Directory);
        }
    }
}
