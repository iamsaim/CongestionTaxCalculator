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

namespace CongestionTaxCalculator.Application.Features.TaxExemptVehicle.Command
{
    public class InsertTaxExemptVehicleCommand : IRequest<bool>
    {
        public InsertTaxExemptVehicleDTO data { get; set; }
    }

    public class InsertTaxExemptVehicleHandler : IRequestHandler<InsertTaxExemptVehicleCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InsertTaxExemptVehicleHandler> _logger;

        public InsertTaxExemptVehicleHandler(ApplicationDbContext context,
            ILogger<InsertTaxExemptVehicleHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(InsertTaxExemptVehicleCommand request, CancellationToken cancellationToken)
        {
            var isCityExists = await _context.Cities.AnyAsync(x=>x.Id == request.data.CityId 
                                                   && x.DeletedDate == null,cancellationToken);

            if (!isCityExists)
            {
                throw new NotFoundException("City not found");
            }

            var taxExemptVehicleEntity = new TaxExemptVehicleEntity()
            {
               CityId = request.data.CityId,
               Type = request.data.Type
            };

            var isAdded = await _context.TaxExemptVehicles.AddAsync(taxExemptVehicleEntity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return isAdded.State == EntityState.Unchanged;
        }

    }
}
