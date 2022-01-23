using Bl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using Domain.ViewModels;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotsController : BaseController
    {
        private readonly ISlotService _slotService;

        public TimeSlotsController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableSlots()
            => Ok(await _slotService.GetAvailableSlots());

        [HttpPost]
        public async Task<IActionResult> BookSlot([FromBody] SlotBookingRequestModel slotBookingRequestModel)
            => CreateResponse(await _slotService.BookSlot(DateTime.Parse(slotBookingRequestModel.StartDate)));
    }
}
