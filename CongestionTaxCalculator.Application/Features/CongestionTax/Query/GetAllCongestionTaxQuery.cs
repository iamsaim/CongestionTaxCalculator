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

namespace CongestionTaxCalculator.Application.Features.CongestionTax.Query
{
    public class GetAllCongestionTaxQuery : IRequest<List<GetAllCongestionTaxResponseDTO>>
    {
        public GetAllCongestionTaxRequestDTO data { get; set; }
    }

    public class GetAllCongestionTaxHandler : IRequestHandler<GetAllCongestionTaxQuery, List<GetAllCongestionTaxResponseDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GetAllCongestionTaxHandler> _logger;

        public GetAllCongestionTaxHandler(ApplicationDbContext context,
            ILogger<GetAllCongestionTaxHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<GetAllCongestionTaxResponseDTO>> Handle(GetAllCongestionTaxQuery request, CancellationToken cancellationToken)
        {
            var isCityExists = await _context.Cities.AnyAsync(x => x.Id == request.data.CityId
                                                                   && x.DeletedDate == null, cancellationToken);

            if (!isCityExists)
            {
                throw new NotFoundException("City not found");
            }

            var isVehicleExists = await _context.Vehicles.AnyAsync(x => x.Id == request.data.VehicleId
                                                                        && x.DeletedDate == null, cancellationToken);

            if (!isVehicleExists)
            {
                throw new NotFoundException("Vehicle not found");
            }

            var listAsync = await _context.CongestionTaxes
                .Where(x => x.CityId == request.data.CityId && x.VehicleId == request.data.VehicleId)
                .Select(x => new GetAllCongestionTaxResponseDTO()
                {
                    Id = x.Id,
                    TaxOn = x.TaxOn,
                    CityId = x.CityId,
                    VehicleId = x.VehicleId,
                    Amount = x.Amount

                })
                .ToListAsync(cancellationToken);

            return listAsync;


        }
    }
}
