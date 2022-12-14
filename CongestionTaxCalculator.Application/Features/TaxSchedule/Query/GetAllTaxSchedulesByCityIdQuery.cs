using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Exceptions;
using CongestionTaxCalculator.Application.Features.TaxExemptVehicle.Query;
using CongestionTaxCalculator.Application.Features.TaxFreeDay.Query;
using CongestionTaxCalculator.Domain.Entities;
using CongestionTaxCalculator.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.CompilerServices;

namespace CongestionTaxCalculator.Application.Features.TaxSchedule.Query
{
    public class GetAllTaxSchedulesByCityIdQuery : IRequest<List<GetAllTaxSchedulesDTO>>
    {
        public Guid CityId { get; set; }
    }

    public class GetAllTaxSchedulesByCityIdHandler : IRequestHandler<GetAllTaxSchedulesByCityIdQuery, List<GetAllTaxSchedulesDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GetAllTaxSchedulesByCityIdHandler> _logger;

        public GetAllTaxSchedulesByCityIdHandler(ApplicationDbContext context,
            ILogger<GetAllTaxSchedulesByCityIdHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<GetAllTaxSchedulesDTO>> Handle(GetAllTaxSchedulesByCityIdQuery request, CancellationToken cancellationToken)
        {
            var isCityExists = await _context.Cities.AnyAsync(x => x.Id == request.CityId
                                                                   && x.DeletedDate == null, cancellationToken);

            if (!isCityExists)
            {
                throw new NotFoundException("City not found");
            }

            var listAsync = await _context.TaxSchedules
                .Where(x => x.CityId == request.CityId)
                .Select(x => new GetAllTaxSchedulesDTO()
                {
                    Id = x.Id,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    CityId = x.CityId,
                    Amount = x.Amount

                })
                .ToListAsync(cancellationToken);

            return listAsync;


        }
    }
}
