using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Business.Contracts;
using PointOfSale.Models;

namespace PointOfSale.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ISaleService _saleService;
        private readonly IPurchaseService _PurchaseService;
        private readonly IMapper _mapper;
        public ReportsController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }
        public IActionResult SalesReport()
        {
            return View();
        }
        public IActionResult PurchasesReport()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReportSale(string startDate, string endDate)
        {
            List<VMSalesReport> vmList = _mapper.Map<List<VMSalesReport>>(await _saleService.Report(startDate, endDate));
            return StatusCode(StatusCodes.Status200OK, new { data = vmList });
        }
        [HttpGet]
        public async Task<IActionResult> ReportPurchase(string startDate, string endDate)
        {
            List<VMPurchasesReport> vmList = _mapper.Map<List<VMPurchasesReport>>(await _PurchaseService.Report(startDate, endDate));
            return StatusCode(StatusCodes.Status200OK, new { data = vmList });
        }
    }
}
