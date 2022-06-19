using MoviePlatform.Models;
using System.Collections.ObjectModel;

namespace MoviePlatform.Service
{
    public interface IReportRepository
    {
        public IReadOnlyCollection<Report> GetReports();
        public Report GetReport(int idReport);
        public void AddReport(Report report);
        public void Save();
    }
    public class ReportRepository : IReportRepository
    {
        MoviePlatformDbContext Context { get; set; }
        public ReportRepository(MoviePlatformDbContext context)
        {
            Context = context;
        }

        public IReadOnlyCollection<Report> GetReports()
        {
            return new ReadOnlyCollection<Report>(Context.Reports.ToList());
        }

        public Report GetReport(int idReport)
        {
            if (!Validate(idReport))
            {
                return new Report();
            }
            return Context.Reports.First(x => x.IdReport == idReport);
        }

        public void AddReport(Report report)
        {
            if (!Context.Users.Any(r => r.IdUser == report.IdReportedUser))
            {
                Console.WriteLine("Given user doesn't exist");
                return;
            }

            Context.Reports.Add(new Report { IdReportedUser = report.IdReportedUser, ReportDescription = "Spam"});
            Save();
        }

        public bool Validate(int idReport)
        {
            if (!Context.Reports.Any(x => x.IdReport == idReport))
            {
                Console.WriteLine("Given report doesn't exist");
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
