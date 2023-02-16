using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.Models;
using SalesApp.ViewModel;

namespace SalesApp.Repository.Interface
{
    public interface IReportRepository
    {
        public Task<ReportVM> Init_Report();
        Task<List<ReportVM>> getAllReportDetail(ReportVM pcm);
        Task<List<ReportVM>> getitemcostingReportDetail(ReportVM pcm);
        
    }
}
