using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ConfigModels;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.Extensions.Options;
using Repository;

namespace Bl
{
    public class SlotService : ISlotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SlotConfig _slotConfig;

        public SlotService(IOptions<SlotConfig> slotConfig, IUnitOfWork unitOfWork)
        {
            _slotConfig = slotConfig.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TimeSlotModel>> GetAvailableSlots()
        {
            var slots = GenerateSlots();
            var bookedSlots = (await _unitOfWork.Repository<BookedSlot>().FindAllAsync()).ToList();
            return slots.Where(slot =>
                    !bookedSlots.Exists(b =>
                        b.StartTimeHour == slot.StartTime.Hour && b.StartTimeMinute == slot.StartTime.Minute)
            );
        }

        public async Task<ResponseModel> BookSlot(DateTime startTime)
        {
            var slots = GenerateSlots();
            var bookedSlots = (await _unitOfWork.Repository<BookedSlot>().FindAllAsync()).ToList();
            var slot = slots.FirstOrDefault(slot =>
                    bookedSlots.Exists(b =>
                        b.StartTimeHour == slot.StartTime.Hour && b.StartTimeMinute == slot.StartTime.Minute) && slot.StartTime == startTime);
            if (slot == null)
            {
                await _unitOfWork.Repository<BookedSlot>().Upsert(new BookedSlot { StartTimeHour = startTime.Hour, StartTimeMinute = startTime.Minute });
                return (await _unitOfWork.CompleteAsync()) ? new ResponseModel { ResponseCode = 201 } : new ResponseModel { Error = "Record could not be saved", ResponseCode = 500 };
            }
            return new ResponseModel { ResponseCode = 409, Error = "This slot is already occupied" };
        }

        private List<TimeSlotModel> GenerateSlots()
        {
            var slots = new List<TimeSlotModel>();
            var startTime = DateTime.Today.Add(new TimeSpan(_slotConfig.StartSlotHour, _slotConfig.StartSlotMinute, 00));
            var endTime = DateTime.Today.Add(new TimeSpan(_slotConfig.EndSlotHour, _slotConfig.EndSlotMinute, 00));

            while (startTime <= endTime.Add(new TimeSpan(0, -_slotConfig.SlotDurationInMinutes, 00)))
            {
                slots.Add(new TimeSlotModel
                {
                    StartTime = startTime
                });
                startTime = startTime.Add(new TimeSpan(0, _slotConfig.SlotDurationInMinutes, 00));
            }

            return slots;
        }
    }
}
