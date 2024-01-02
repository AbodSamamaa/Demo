using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PointOfSale.Business.Contracts;
using PointOfSale.Business.Services;
using PointOfSale.Model;
using PointOfSale.Models;
using PointOfSale.Utilities.Response;
using System.Security.Claims;
namespace PointOfSale.Controllers
{
    [Authorize]
    public class PurchasesController : Controller
    {
        private readonly ITypeDocumentPurchaseService _typeDocumentPurchaseService;
        private readonly IPurchaseService _PurchaseService;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public PurchasesController(ITypeDocumentPurchaseService typeDocumentPurchaseService,
            IPurchaseService PurchaseService, IMapper mapper, IConverter converter)
        {
            _typeDocumentPurchaseService = typeDocumentPurchaseService;
            _PurchaseService = PurchaseService;
            _mapper = mapper;
            _converter = converter;
        }

        public IActionResult NewPurchase()
        {
            return View();
        }

        public IActionResult PurchasesHistory()
        {
            return View();
        }
        public IActionResult TypeDocument()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ListTypeDocumentPurchase()
        {
            List<VMTypeDocumentPurchase> vmListTypeDocumentPurchase = _mapper.Map<List<VMTypeDocumentPurchase>>(await _typeDocumentPurchaseService.ListForTypeDocumentPurchases());
            return StatusCode(StatusCodes.Status200OK, vmListTypeDocumentPurchase);
        }
        [HttpGet]
        public async Task<IActionResult> GetCategory(string search)
        {
            try
            {
                List<VMCategory> vmListProducts = _mapper.Map<List<VMCategory>>(await _PurchaseService.GetCategories(search));
                return StatusCode(StatusCodes.Status200OK, vmListProducts);
            }
            catch (Exception ex)
            {
                // Handle the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request .");
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegisterPurchase([FromBody] VMPurchase model)
        {
            GenericResponse<VMPurchase> gResponse = new GenericResponse<VMPurchase>();
            try
            {

                ClaimsPrincipal claimuser = HttpContext.User;

                string idUsuario = claimuser.Claims
                        .Where(c => c.Type == ClaimTypes.NameIdentifier)
                        .Select(c => c.Value).SingleOrDefault();

                model.IdUsers = int.Parse(idUsuario);


                Purchase Purchase_created = await _PurchaseService.Register(_mapper.Map<Purchase>(model));
                model = _mapper.Map<VMPurchase>(Purchase_created);

                gResponse.State = true;
                gResponse.Object = model;
            }
            catch (Exception ex)
            {
                gResponse.State = false;
                gResponse.Message = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
        [HttpGet]
		public async Task<IActionResult> GetTypeDocumentPurchase()
		{
			try
			{
				List<VMTypeDocumentPurchase> listTypeDocumentPurchase = _mapper.Map<List<VMTypeDocumentPurchase>>(await _typeDocumentPurchaseService.ListForTypeDocumentPurchases());
				return StatusCode(StatusCodes.Status200OK, new { data = listTypeDocumentPurchase });
			}
			catch (Exception ex)
			{
				throw; // Rethrow the exception if you want it to be handled higher up the call stack
			}
		}

		[HttpPost]
        public async Task<IActionResult> CreateTypeDocumentPurchase([FromForm] string model)
        {
            GenericResponse<VMTypeDocumentPurchase> gResponse = new GenericResponse<VMTypeDocumentPurchase>();
            try
            {
                VMTypeDocumentPurchase VMTypeDocumentPurchase = JsonConvert.DeserializeObject<VMTypeDocumentPurchase>(model);

                TypeDocumentPurchase usuario_creado = await _typeDocumentPurchaseService.Add(_mapper.Map<TypeDocumentPurchase>(VMTypeDocumentPurchase));

                VMTypeDocumentPurchase = _mapper.Map<VMTypeDocumentPurchase>(usuario_creado);

                gResponse.State = true;
                gResponse.Object = VMTypeDocumentPurchase;
            }
            catch (Exception ex)
            {
                gResponse.State = false;
                gResponse.Message = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTypeDocumentPurchase([FromForm] string model)
        {
            GenericResponse<VMTypeDocumentPurchase> gResponse = new GenericResponse<VMTypeDocumentPurchase>();
            try
            {
                VMTypeDocumentPurchase VMTypeDocumentPurchase = JsonConvert.DeserializeObject<VMTypeDocumentPurchase>(model);

                TypeDocumentPurchase user_edited = await _typeDocumentPurchaseService.Edit(_mapper.Map<TypeDocumentPurchase>(VMTypeDocumentPurchase));

                VMTypeDocumentPurchase = _mapper.Map<VMTypeDocumentPurchase>(user_edited);

                gResponse.State = true;
                gResponse.Object = VMTypeDocumentPurchase;
            }
            catch (Exception ex)
            {
                gResponse.State = false;
                gResponse.Message = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTypeDocumentPurchase(int IdTypeDocumentPurchase)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.State = await _typeDocumentPurchaseService.Delete(IdTypeDocumentPurchase);
            }
            catch (Exception ex)
            {
                gResponse.State = false;
                gResponse.Message = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
        [HttpGet]
        public async Task<IActionResult> History(string PurchaseNumber, string startDate, string endDate)
        {

            List<VMPurchase> vmHistorySale = _mapper.Map<List<VMPurchase>>(await _PurchaseService.PurchaseHistory(PurchaseNumber, startDate, endDate));
            return StatusCode(StatusCodes.Status200OK, vmHistorySale);
        }
        public IActionResult ShowPDFPurchase(string PurchaseNumber)
        {
            string urlTemplateView = $"{this.Request.Scheme}://{this.Request.Host}/Template/PDFPurchase?PurchaseNumber={PurchaseNumber}";

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects = {
                    new ObjectSettings(){
                        Page = urlTemplateView
                    }
                }
            };
            var archivoPDF = _converter.Convert(pdf);
            return File(archivoPDF, "application/pdf");
        }
    }
}
