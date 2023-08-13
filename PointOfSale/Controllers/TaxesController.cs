using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PointOfSale.Business.Contracts;
using PointOfSale.Business.Services;
using PointOfSale.Model;
using PointOfSale.Models;
using PointOfSale.Utilities.Response;

namespace PointOfSale.Controllers
{
	public class TaxesController : Controller
	{
		private readonly IRolService _rolService;
		private readonly ITaxService _TaxService;
		private readonly IMapper _mapper;
		public TaxesController(ITaxService TaxService, IRolService rolService, IMapper mapper)
		{
			_TaxService = TaxService;
			_rolService = rolService;
			_mapper = mapper;
		}

		public IActionResult Tax()
		{
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> GetTaxes()
		{
			List<VMTax> listTaxes = _mapper.Map<List<VMTax>>(await _TaxService.List());
			return StatusCode(StatusCodes.Status200OK, new { data = listTaxes });
		}
		[HttpGet]
		public async Task<IActionResult> GetTaxesForProducts()
		{
			List<VMTax> listTaxes = _mapper.Map<List<VMTax>>(await _TaxService.ListForProducts());
			return StatusCode(StatusCodes.Status200OK, new { data = listTaxes });
		}
		[HttpPost]
		public async Task<IActionResult> CreateTax([FromForm]  string model)
		{
			GenericResponse<VMTax> gResponse = new GenericResponse<VMTax>();
			try
			{
				VMTax VMTax = JsonConvert.DeserializeObject<VMTax>(model);

				Tax usuario_creado = await _TaxService.Add(_mapper.Map<Tax>(VMTax));

				VMTax = _mapper.Map<VMTax>(usuario_creado);

				gResponse.State = true;
				gResponse.Object = VMTax;
			}
			catch (Exception ex)
			{
				gResponse.State = false;
				gResponse.Message = ex.Message;
			}

			return StatusCode(StatusCodes.Status200OK, gResponse);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateTax([FromForm] IFormFile photo, [FromForm] string model)
		{
			GenericResponse<VMTax> gResponse = new GenericResponse<VMTax>();
			try
			{
				VMTax VMTax = JsonConvert.DeserializeObject<VMTax>(model);

				Tax user_edited = await _TaxService.Edit(_mapper.Map<Tax>(VMTax));

				VMTax = _mapper.Map<VMTax>(user_edited);

				gResponse.State = true;
				gResponse.Object = VMTax;
			}
			catch (Exception ex)
			{
				gResponse.State = false;
				gResponse.Message = ex.Message;
			}

			return StatusCode(StatusCodes.Status200OK, gResponse);
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteTax(int IdTax)
		{
			GenericResponse<string> gResponse = new GenericResponse<string>();
			try
			{
				gResponse.State = await _TaxService.Delete(IdTax);
			}
			catch (Exception ex)
			{
				gResponse.State = false;
				gResponse.Message = ex.Message;
			}

			return StatusCode(StatusCodes.Status200OK, gResponse);
		}
	}
}