using MediatR;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Exceptions;
using CongestionTaxCalculator.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CongestionTaxCalculator.Application.Features.TaxFreeDay.Command
{
    public class UpdateTaxFreeDayCommand : IRequest<bool>
    {
        public UpdateTaxFreeDayDTO data { get; set; }
    }

    public class UpdateTaxFreeDayHandler : IRequestHandler<UpdateTaxFreeDayCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UpdateTaxFreeDayHandler> _logger;

        public UpdateTaxFreeDayHandler(ApplicationDbContext context,
            ILogger<UpdateTaxFreeDayHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(UpdateTaxFreeDayCommand request, CancellationToken cancellationToken)
        {
            var isCityExists = await _context.Cities.AnyAsync(x => x.Id == request.data.CityId
                                                                   && x.DeletedDate == null, cancellationToken);

            if (!isCityExists)
            {
                throw new NotFoundException("City not found");
            }

            var taxFreeDayEntity = await _context.TaxFreeDays.FirstOrDefaultAsync(x => x.Id == request.data.Id
                && x.DeletedDate == null, cancellationToken);

            if (taxFreeDayEntity == null)
            {
                throw new NotFoundException("Tax Exempt Vehicle not found");
            }

            var convertedStartTime = new DateTime(request.data.StartTime.Year,
                request.data.StartTime.Month, request.data.StartTime.Day, 0, 0, 0);

            var convertedEndTime = new DateTime(request.data.EndTime.Year,
                request.data.EndTime.Month, request.data.EndTime.Day, 0, 0, 0);

            taxFreeDayEntity.CityId = request.data.CityId;
            taxFreeDayEntity.StartTime = convertedStartTime;
            taxFreeDayEntity.EndTime = convertedEndTime;
            taxFreeDayEntity.IsPublicHoliday = request.data.IsPublicHoliday;

            _context.TaxFreeDays.Update(taxFreeDayEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
