using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.ViewModels;

namespace Bl
{
    public interface ISlotService
    {
        Task<IEnumerable<TimeSlotModel>> GetAvailableSlots();
        public Task<ResponseModel> BookSlot(DateTime startTime);
    }
}
