using System.Threading.Tasks;

using Data;

using Infrastructure.Constants;

using Quartz;

using Services.Implementations;

namespace API.Scheduler
{
    public class SchedulerReader : IJob
    {
        public SchedulerReader(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        public async Task Execute(IJobExecutionContext context)
        {
            BaseCRUDService baseCRUD = new BaseCRUDService(DbContext);
            await baseCRUD.ReadFile(DirectoryConstant.Directory);
        }
    }
}
