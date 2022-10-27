using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Exceptions;
using CongestionTaxCalculator.Application.Features.TaxExemptVehicle.Query;
using CongestionTaxCalculator.Domain.Entities;
using CongestionTaxCalculator.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.CompilerServices;

namespace CongestionTaxCalculator.Application.Features.TaxFreeDay.Query
{
    public class GetAllTaxFreeDaysByCityIdQuery : IRequest<List<GetAllTaxFreeDaysDTO>>
    {
        public Guid CityId { get; set; }
    }

    public class GetAllTaxFreeDaysByCityIdQueryHandler : IRequestHandler<GetAllTaxFreeDaysByCityIdQuery, List<GetAllTaxFreeDaysDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GetAllTaxFreeDaysByCityIdQueryHandler> _logger;

        public GetAllTaxFreeDaysByCityIdQueryHandler(ApplicationDbContext context,
            ILogger<GetAllTaxFreeDaysByCityIdQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<GetAllTaxFreeDaysDTO>> Handle(GetAllTaxFreeDaysByCityIdQuery request, CancellationToken cancellationToken)
        {
            var isCityExists = await _context.Cities.AnyAsync(x => x.Id == request.CityId
                                                                   && x.DeletedDate == null, cancellationToken);

            if (!isCityExists)
            {
                throw new NotFoundException("City not found");
            }

            var listAsync = await _context.TaxFreeDays
                .Where(x => x.CityId == request.CityId)
                .Select(x => new GetAllTaxFreeDaysDTO()
                {
                    Id = x.Id,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    CityId = x.CityId,
                    IsPublicHoliday = x.IsPublicHoliday

                })
                .ToListAsync(cancellationToken);

            return listAsync;


        }
    }
}
