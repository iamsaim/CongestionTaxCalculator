using MediatR;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Exceptions;
using CongestionTaxCalculator.Domain.Entities;
using CongestionTaxCalculator.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CongestionTaxCalculator.Application.Features.TaxFreeDay.Command
{
    public class InsertTaxFreeDayCommand : IRequest<bool>
    {
        public InsertTaxFreeDayDTO data { get; set; }
    }

    public class InsertTaxFreeDayHandler : IRequestHandler<InsertTaxFreeDayCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InsertTaxFreeDayHandler> _logger;

        public InsertTaxFreeDayHandler(ApplicationDbContext context,
            ILogger<InsertTaxFreeDayHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(InsertTaxFreeDayCommand request, CancellationToken cancellationToken)
        {
            var isCityExists = await _context.Cities.AnyAsync(x => x.Id == request.data.CityId
                                                                   && x.DeletedDate == null, cancellationToken);

            if (!isCityExists)
            {
                throw new NotFoundException("City not found");
            }

            var convertedStartTime = new DateTime(request.data.StartTime.Year,
                request.data.StartTime.Month, request.data.StartTime.Day, 0, 0, 0);

            var convertedEndTime = new DateTime(request.data.EndTime.Year,
                request.data.EndTime.Month, request.data.EndTime.Day, 0, 0, 0);

            var taxFreeDayEntity = new TaxFreeDayEntity()
            {
                CityId = request.data.CityId,
                StartTime = convertedStartTime,
                EndTime = convertedEndTime,
                IsPublicHoliday = request.data.IsPublicHoliday
            };

            var isAdded = await _context.TaxFreeDays.AddAsync(taxFreeDayEntity, cancellationToken);


            await _context.SaveChangesAsync(cancellationToken);

            return isAdded.State == EntityState.Unchanged;
        }

    }
}
