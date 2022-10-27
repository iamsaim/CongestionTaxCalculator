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

namespace CongestionTaxCalculator.Application.Features.Vehicle.Command
{ 
    public class InsertVehicleCommand : IRequest<bool>
    {
        public InsertVehicleDTO data { get; set; }
    }

    public class InsertVehicleHandler : IRequestHandler<InsertVehicleCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InsertVehicleHandler> _logger;

        public InsertVehicleHandler(ApplicationDbContext context,
            ILogger<InsertVehicleHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(InsertVehicleCommand request, CancellationToken cancellationToken)
        {
            

            var taxExemptVehicleEntity = new VehicleEntity()
            {
                LicenceNumber = request.data.LicenceNumber,
                VehicleType = request.data.VehicleType
            };

            var isAdded = await _context.Vehicles.AddAsync(taxExemptVehicleEntity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return isAdded.State == EntityState.Unchanged;
        }

    }
}
