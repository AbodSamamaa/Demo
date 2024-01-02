using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PointOfSale.Business.Contracts;
using PointOfSale.Business.Services;
using PointOfSale.Model;
using PointOfSale.Models;
using PointOfSale.Utilities.Response;

namespace PointOfSale.Controllers
{
	[Authorize]
	public class PaymentController : Controller
	{
		private readonly IRolService _rolService;
		private readonly IPaymentService _PaymentService;
		private readonly IMapper _mapper;

		public PaymentController(IPaymentService PaymentService, IRolService rolService, IMapper mapper)
		{
			_PaymentService = PaymentService;
			_rolService = rolService;
			_mapper = mapper;
		}
		public IActionResult PaymentWay()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetPaymentes()
		{
			try
			{
				List<VMPayment> listPaymentes = _mapper.Map<List<VMPayment>>(await _PaymentService.List());
				return StatusCode(StatusCodes.Status200OK, new { data = listPaymentes });
			}
			catch (NullReferenceException ex)
			{
				// Handle the exception here, log it, and return an appropriate response
				return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
			}
		}
        [HttpGet]
        public async Task<IActionResult> GetActivePaymentes()
        {
            try
            {
                List<VMPayment> listPaymentes = _mapper.Map<List<VMPayment>>(await _PaymentService.ListForActive());
                return StatusCode(StatusCodes.Status200OK, new { data = listPaymentes });
            }
            catch (NullReferenceException ex)
            {
                // Handle the exception here, log it, and return an appropriate response
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }
        [HttpPost]
		public async Task<IActionResult> CreatePayment([FromForm] string model)
		{
			GenericResponse<VMPayment> gResponse = new GenericResponse<VMPayment>();
			try
			{
				VMPayment VMPayment = JsonConvert.DeserializeObject<VMPayment>(model);
				VMPayment.IdPayment = null;

                Payment usuario_creado = await _PaymentService.Add(_mapper.Map<Payment>(VMPayment));

				VMPayment = _mapper.Map<VMPayment>(usuario_creado);

				gResponse.State = true;
				gResponse.Object = VMPayment;
			}
			catch (Exception ex)
			{
				gResponse.State = false;
				gResponse.Message = ex.Message;
			}

			return StatusCode(StatusCodes.Status200OK, gResponse);
		}
		[HttpPut]
		public async Task<IActionResult> UpdatePayment([FromForm] string model)
		{
			GenericResponse<VMPayment> gResponse = new GenericResponse<VMPayment>();
			try
			{
				VMPayment VMPayment = JsonConvert.DeserializeObject<VMPayment>(model);

				Payment user_edited = await _PaymentService.Edit(_mapper.Map<Payment>(VMPayment));

				VMPayment = _mapper.Map<VMPayment>(user_edited);

				gResponse.State = true;
				gResponse.Object = VMPayment;
			}
			catch (Exception ex)
			{
				gResponse.State = false;
				gResponse.Message = ex.Message;
			}

			return StatusCode(StatusCodes.Status200OK, gResponse);
		}
        [HttpDelete]
        public async Task<IActionResult> DeletePayment(int IdPayment)
        {
			GenericResponse<string> gResponse = new GenericResponse<string>();
			try
			{
				gResponse.State = await _PaymentService.Delete(IdPayment);
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
