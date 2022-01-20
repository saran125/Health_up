using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.Report.Any(e => e.Report_id == id);
        }
        public List<MedicalReport> GetAllReports()
        {
            List<MedicalReport> AllReports = new List<MedicalReport>();
            AllReports = _context.Report.ToList();
            return AllReports;
        }
        public MedicalReport GetReportById(string id)
        {
            MedicalReport report = _context.Report.Where(e => e.Report_id == id).FirstOrDefault();
            return report;
        }
        public bool UpdateReport(MedicalReport reportID)
        {
            bool update = true;
            _context.Attach(reportID).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
                update = true;
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!ReportExist(reportID.Report_id))
                {
                    update = false;
                }
                else throw;
            }
            return update;
        }
        public bool DeleteReport(MedicalReport reportID)
        {
            bool delete = true;
            try
            {
                _context.Remove(reportID);
                _context.SaveChanges();
                delete = true;
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!ReportExist(reportID.Report_id))
                {
                    delete = false;
                }
                else throw;
            }
            return delete;
        }
    }
}
