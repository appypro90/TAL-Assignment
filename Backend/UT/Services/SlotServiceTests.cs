using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bl;
using Domain.ConfigModels;
using Domain.Models;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Repository;

namespace UT.Services
{
    public class SlotServiceTests
    {
        private readonly ISlotService _slotService;
        private readonly int _startHour = 11;
        private readonly int _startMinute = 30;

        public SlotServiceTests()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Repository<BookedSlot>().FindAllAsync()).Returns(Task.FromResult(new List<BookedSlot>
            {
                new BookedSlot
                {
                    Id = Guid.NewGuid().ToString(),
                    StartTimeHour = _startHour,
                    StartTimeMinute = _startMinute
                }
            } as IEnumerable<BookedSlot>));

            mockUnitOfWork.Setup(uow => uow.CompleteAsync()).Returns(Task.FromResult(true));

            var mockSlotConfig = Options.Create(new SlotConfig
            {
                SlotDurationInMinutes = 30,
                StartSlotHour = 9,
                StartSlotMinute = 0,
                EndSlotHour = 17,
                EndSlotMinute = 0
            });
            _slotService = new SlotService(mockSlotConfig,  mockUnitOfWork.Object);
        }

        [TestCase]
        public async Task ShouldGetData()
        {
            var result = await _slotService.GetAvailableSlots();
            Assert.NotNull(result);
            Assert.AreEqual(15, result.Count());
        }

        [TestCase]
        public async Task ShouldAddANewSlotSuccessfully()
        {
            var result = await _slotService.BookSlot(DateTime.Parse("2022-01-23T09:00:00+11:00"));
            Assert.NotNull(result);
            Assert.AreEqual(201, result.ResponseCode);
        }

        [TestCase]
        public async Task ShouldNotAddANewSlotSuccessfullyForExistingSlot()
        {
            var result = await _slotService.BookSlot(DateTime.Parse("2022-01-23T11:30:00+11:00"));
            Assert.NotNull(result);
            Assert.AreEqual(409, result.ResponseCode);
        }
    }
}
