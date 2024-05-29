//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using CanvasHub.Models;
//using Microsoft.EntityFrameworkCore;
//using CanvasHub.Services.Interfaces;

//namespace CanvasHub.Services
//{
//    public class ReportService : IReportService
//    {
//        private readonly CanvasHubContext _context;

//        public ReportService(CanvasHubContext context)
//        {
//            _context = context;
//        }

//        public async Task<PaymentsReport> GeneratePaymentsReportAsync()
//        {
//            var users = await _context.Users.ToListAsync();
//            var report = new PaymentsReport
//            {
//                Payments = users.Select(u => new PaymentDetail
//                {
//                    UserName = u.UserName,
//                    TotalAmount = u.Subscription
//                }).ToList()
//            };
//            return report;
//        }

//        public async Task<ProfitReport> GenerateProfitReportAsync()
//        {
//            // Assume we have an Incomes and Costs table in the database
//            var incomes = await _context.Incomes.ToListAsync();
//            var costs = await _context.Costs.ToListAsync();

//            var totalIncome = incomes.Sum(i => i.Amount);
//            var totalCost = costs.Sum(c => c.Amount);
//            var totalProfit = totalIncome - totalCost;

//            var report = new ProfitReport
//            {
//                TotalIncome = totalIncome,
//                TotalCost = totalCost,
//                TotalProfit = totalProfit
//            };
//            return report;
//        }

//        public async Task<List<ResourceCalendarReport>> GenerateResourceCalendarReportAsync()
//        {
//            var resources = await _context.Resources
//                .Include(r => r.Event)
//                .ToListAsync();

//            var report = resources.Select(r => new ResourceCalendarReport
//            {
//                ResourceId = r.ResourceId,
//                ResourceType = r.ResourceType,
//                ResourceName = r.ResourceName,
//                Schedule = r.Event != null ? r.Event.EventName : "No Event"
//            }).ToList();

//            return report;
//        }

//        public async Task<List<ResourceAvailabilityReport>> GenerateResourceAvailabilityReportAsync(DateTime startDate, DateTime endDate)
//        {
//            var resources = await _context.Resources
//                .Include(r => r.Event)
//                .Where(r => r.Event.BookedDate < startDate || r.Event.BookedDate > endDate)
//                .ToListAsync();

//            var report = resources.Select(r => new ResourceAvailabilityReport
//            {
//                ResourceId = r.ResourceId,
//                ResourceType = r.ResourceType,
//                ResourceName = r.ResourceName,
//                AvailableFrom = startDate,
//                AvailableTo = endDate
//            }).ToList();

//            return report;
//        }
//    }
