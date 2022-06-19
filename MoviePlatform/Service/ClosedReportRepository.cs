using MoviePlatform.Models;
using System.Collections.ObjectModel;

namespace MoviePlatform.Service
{
    public interface IClosedReportRepository
    {
        public IReadOnlyCollection<ClosedReport> GetClosedReports();
        public IReadOnlyCollection<ClosedReport> GetClosedReportsForAdmin(int idModerator);
        public void AddClosedReport(ClosedReport closedReport);
        public void DeleteClosedReport(int idClosedReport);
        public void Save();
    }
    public class ClosedReportRepository : IClosedReportRepository
    {
        MoviePlatformDbContext Context { get; set; }
        public ClosedReportRepository(MoviePlatformDbContext context)
        {
            Context = context;
        }

        public IReadOnlyCollection<ClosedReport> GetClosedReports()
        {
            return new ReadOnlyCollection<ClosedReport>(Context.ClosedReports.ToList());
        }

        public IReadOnlyCollection<ClosedReport> GetClosedReportsForAdmin(int idModerator)
        {
            return new ReadOnlyCollection<ClosedReport>(Context.ClosedReports.Where(r => r.IdUser == idModerator).ToList());
        }

        public void AddClosedReport(ClosedReport closedReport)
        {
            if (!Context.Users.Any(r => r.IdUser == closedReport.IdUser || !Context.Reports.Any(r => r.IdReport == closedReport.IdReport)))
            {
                Console.WriteLine("Given user or report doesn't exist");
                return;
            }

            if (closedReport.Moderator.UserType != UserType.Moderator)
            {
                Console.WriteLine("Only moderators can add movie to favorites");
                return;
            }
            Context.ClosedReports.Add(new ClosedReport { IdReport = closedReport.IdReport, Action = closedReport.Action, IdReportedUser = closedReport.IdReportedUser, IdModerator = closedReport.IdModerator });
            Save();
        }

        public void DeleteClosedReport(int idClosedReport)
        {
            if (!Validate(idClosedReport))
            {
                return;
            }
            Context.ClosedReports.Remove(Context.ClosedReports.First(r => r.IdReport == idClosedReport));
            Save();
        }

        public bool Validate(int idClosedReport)
        {
            if (!Context.ClosedReports.Any(x => x.IdReport == idClosedReport))
            {
                return false;
            }
            return true;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
