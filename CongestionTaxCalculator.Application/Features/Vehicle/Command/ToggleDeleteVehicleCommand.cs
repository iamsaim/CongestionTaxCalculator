using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Exceptions;
using CongestionTaxCalculator.Application.Features.TaxExemptVehicle.Command;
using CongestionTaxCalculator.Domain.Entities;
using CongestionTaxCalculator.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.CompilerServices;

namespace CongestionTaxCalculator.Application.Features.Vehicle.Command
{
    public class ToggleDeleteVehicleCommand : IRequest<bool>
    {
        public ToggleDeleteDTO data { get; set; }
    }

    public class ToggleDeleteVehicleHandler : IRequestHandler<ToggleDeleteVehicleCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ToggleDeleteVehicleHandler> _logger;

        public ToggleDeleteVehicleHandler(ApplicationDbContext context,
            ILogger<ToggleDeleteVehicleHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(ToggleDeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicleEntity = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == request.data.Id
                , cancellationToken);

            if (vehicleEntity == null)
            {
                throw new NotFoundException("Vehicle not found");
            }

            vehicleEntity.DeletedDate = request.data.Delete == true ? DateTime.Now : null;

            _context.Vehicles.Update(vehicleEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
