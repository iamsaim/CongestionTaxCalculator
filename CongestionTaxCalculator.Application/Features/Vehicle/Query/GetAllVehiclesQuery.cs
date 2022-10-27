using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Exceptions;
using CongestionTaxCalculator.Application.Features.City.Query;
using CongestionTaxCalculator.Application.Features.TaxExemptVehicle.Query;
using CongestionTaxCalculator.Domain.Entities;
using CongestionTaxCalculator.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.CompilerServices;

namespace CongestionTaxCalculator.Application.Features.Vehicle.Query
{
    public class GetAllVehiclesQuery : IRequest<List<GetAllVehiclesDTO>>
    {
    }

    public class GetAllVehiclesHandler : IRequestHandler<GetAllVehiclesQuery, List<GetAllVehiclesDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GetAllVehiclesHandler> _logger;

        public GetAllVehiclesHandler(ApplicationDbContext context,
            ILogger<GetAllVehiclesHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<GetAllVehiclesDTO>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var listAsync = await _context.Vehicles
                .Select(x => new GetAllVehiclesDTO()
                {
                    Id = x.Id,
                    LicenceNumber = x.LicenceNumber,
                    VehicleType = x.VehicleType

                })
                .ToListAsync(cancellationToken);

            return listAsync;


        }
    }
}
