using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Exceptions;
using CongestionTaxCalculator.Domain.Entities;
using CongestionTaxCalculator.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.CompilerServices;

namespace CongestionTaxCalculator.Application.Features.TaxExemptVehicle.Query
{
    public class GetTaxExemptVehicleByCityIdQuery : IRequest<List<GetTaxExemptVehicleDTO>>
    {
        public Guid CityId { get; set; }
    }

    public class GetTaxExemptVehicleByCityIdQueryHandler : IRequestHandler<GetTaxExemptVehicleByCityIdQuery, List<GetTaxExemptVehicleDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GetTaxExemptVehicleByCityIdQueryHandler> _logger;

        public GetTaxExemptVehicleByCityIdQueryHandler(ApplicationDbContext context,
            ILogger<GetTaxExemptVehicleByCityIdQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<GetTaxExemptVehicleDTO>> Handle(GetTaxExemptVehicleByCityIdQuery request, CancellationToken cancellationToken)
        {
            var isCityExists = await _context.Cities.AnyAsync(x => x.Id == request.CityId
                                                                   && x.DeletedDate == null, cancellationToken);

            if (!isCityExists)
            {
                throw new NotFoundException("City not found");
            }

            var listAsync = await _context.TaxExemptVehicles
                .Where(x => x.CityId == request.CityId)
                .Select(x => new GetTaxExemptVehicleDTO()
                {
                    Id = x.Id,
                    Type = x.Type,
                    CityId = x.CityId

                })
                .ToListAsync(cancellationToken);

            return listAsync;

            
        }
    }
}
