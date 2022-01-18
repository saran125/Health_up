using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;

namespace Health_up.Services
{
    public class ReportService
    {
        private Models.HealthUPDbContext _context;
        public ReportService(Models.HealthUPDbContext context)
        {
            _context = context;
        }
        public bool AddReport(MedicalReport report)
        {
            if(ReportExist(report.Report_id))
            {
                return false;
            }
            else
            {
                _context.Add(report);
                _context.SaveChanges();
                return true;
            }
        }
        private bool ReportExist(string id)
        {
            return _context.Reports.Any(e => e.Report_id == id);
        }
        private List<MedicalReport> GetAllReports()
        {
            List<MedicalReport> AllReports = new List<MedicalReport>();
            AllReports = _context.Reports.ToList();
            return AllReports;
        }
        private MedicalReport GetReportById(string id)
        {
            MedicalReport report = _context.Reports.Where(e => e.Report_id == id).FirstOrDefault();
            return report;
        }
    }
}
