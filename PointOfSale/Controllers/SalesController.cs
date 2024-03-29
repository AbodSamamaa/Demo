﻿using AutoMapper;
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
    public class SalesController : Controller
    {
        private readonly ITypeDocumentSaleService _typeDocumentSaleService;
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public SalesController(ITypeDocumentSaleService typeDocumentSaleService,
            ISaleService saleService, IMapper mapper, IConverter converter)
        {
            _typeDocumentSaleService = typeDocumentSaleService;
            _saleService = saleService;
            _mapper = mapper;
            _converter = converter;
        }
        public IActionResult NewSale()
        {
            return View();
        }

        public IActionResult SalesHistory()
        {
            return View();
        }
		public IActionResult TypeDocument()
		{
			return View();
		}
		[HttpGet]
        public async Task<IActionResult> ListTypeDocumentSale()
        {
            List<VMTypeDocumentSale> vmListTypeDocumentSale = _mapper.Map<List<VMTypeDocumentSale>>(await _typeDocumentSaleService.ListForTypeDocumentSales());
            return StatusCode(StatusCodes.Status200OK, vmListTypeDocumentSale);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(string search)
        {
            List<VMProduct> vmListProducts = _mapper.Map<List<VMProduct>>(await _saleService.GetProducts(search));
            return StatusCode(StatusCodes.Status200OK, vmListProducts);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterSale([FromBody] VMSale model)
        {
            GenericResponse<VMSale> gResponse = new GenericResponse<VMSale>();
            try
            {

                ClaimsPrincipal claimuser = HttpContext.User;

                string idUsuario = claimuser.Claims
                        .Where(c => c.Type == ClaimTypes.NameIdentifier)
                        .Select(c => c.Value).SingleOrDefault();

                model.IdUsers = int.Parse(idUsuario);


                Sale sale_created = await _saleService.Register(_mapper.Map<Sale>(model));
                model = _mapper.Map<VMSale>(sale_created);

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
		public async Task<IActionResult> GetTypeDocumentSale()
		{
			try
			{
				List<VMTypeDocumentSale> listTypeDocumentSale = _mapper.Map<List<VMTypeDocumentSale>>(await _typeDocumentSaleService.ListForTypeDocumentSales());
				return StatusCode(StatusCodes.Status200OK, new { data = listTypeDocumentSale });
			}
			catch (Exception ex)
			{
				throw; // Rethrow the exception if you want it to be handled higher up the call stack
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateTypeDocumentSale([FromForm] string model)
		{
			GenericResponse<VMTypeDocumentSale> gResponse = new GenericResponse<VMTypeDocumentSale>();
			try
			{
				VMTypeDocumentSale VMTypeDocumentSale = JsonConvert.DeserializeObject<VMTypeDocumentSale>(model);

				TypeDocumentSale usuario_creado = await _typeDocumentSaleService.Add(_mapper.Map<TypeDocumentSale>(VMTypeDocumentSale));

				VMTypeDocumentSale = _mapper.Map<VMTypeDocumentSale>(usuario_creado);

				gResponse.State = true;
				gResponse.Object = VMTypeDocumentSale;
			}
			catch (Exception ex)
			{
				gResponse.State = false;
				gResponse.Message = ex.Message;
			}

			return StatusCode(StatusCodes.Status200OK, gResponse);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateTypeDocumentSale([FromForm] string model)
		{
			GenericResponse<VMTypeDocumentSale> gResponse = new GenericResponse<VMTypeDocumentSale>();
			try
			{
				VMTypeDocumentSale VMTypeDocumentSale = JsonConvert.DeserializeObject<VMTypeDocumentSale>(model);

				TypeDocumentSale user_edited = await _typeDocumentSaleService.Edit(_mapper.Map<TypeDocumentSale>(VMTypeDocumentSale));

				VMTypeDocumentSale = _mapper.Map<VMTypeDocumentSale>(user_edited);

				gResponse.State = true;
				gResponse.Object = VMTypeDocumentSale;
			}
			catch (Exception ex)
			{
				gResponse.State = false;
				gResponse.Message = ex.Message;
			}

			return StatusCode(StatusCodes.Status200OK, gResponse);
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteTypeDocumentSale(int IdTypeDocumentSale)
		{
			GenericResponse<string> gResponse = new GenericResponse<string>();
			try
			{
				gResponse.State = await _typeDocumentSaleService.Delete(IdTypeDocumentSale);
			}
			catch (Exception ex)
			{
				gResponse.State = false;
				gResponse.Message = ex.Message;
			}

			return StatusCode(StatusCodes.Status200OK, gResponse);
		}

		[HttpGet]
        public async Task<IActionResult> History(string saleNumber, string startDate, string endDate)
        {

            List<VMSale> vmHistorySale= _mapper.Map<List<VMSale>>(await _saleService.SaleHistory(saleNumber, startDate, endDate));
            return StatusCode(StatusCodes.Status200OK, vmHistorySale);
        }

        public IActionResult ShowPDFSale(string saleNumber)
        {
            string urlTemplateView = $"{this.Request.Scheme}://{this.Request.Host}/Template/PDFSale?saleNumber={saleNumber}";

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