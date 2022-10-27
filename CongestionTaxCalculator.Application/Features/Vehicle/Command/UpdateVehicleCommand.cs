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
    public class UpdateVehicleCommand : IRequest<bool>
    {
        public UpdateVehicleDTO data { get; set; }
    }

    public class UpdateVehicleHandler : IRequestHandler<UpdateVehicleCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UpdateVehicleHandler> _logger;

        public UpdateVehicleHandler(ApplicationDbContext context,
            ILogger<UpdateVehicleHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {

            var vehicleEntity = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == request.data.Id
                && x.DeletedDate == null, cancellationToken);

            if (vehicleEntity == null)
            {
                throw new NotFoundException("Vehicle not found");
            }

            vehicleEntity.LicenceNumber = request.data.LicenceNumber;
            vehicleEntity.VehicleType = request.data.VehicleType;

            _context.Vehicles.Update(vehicleEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
