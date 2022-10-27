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

namespace CongestionTaxCalculator.Application.Features.CongestionTax.Command
{
    public class UpdateCongestionTaxCommand : IRequest<bool>
    {
        public UpdateCongestionTaxDTO data { get; set; }
        public double TaxAmount {get; set; }
    }

    public class UpdateCongestionTaxHandler : IRequestHandler<UpdateCongestionTaxCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UpdateCongestionTaxHandler> _logger;

        public UpdateCongestionTaxHandler(ApplicationDbContext context,
            ILogger<UpdateCongestionTaxHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(UpdateCongestionTaxCommand request, CancellationToken cancellationToken)
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

            var congestionTaxEntity = await _context.CongestionTaxes.FirstOrDefaultAsync(x => x.Id == request.data.Id
                && x.DeletedDate == null, cancellationToken);

            if (congestionTaxEntity == null)
            {
                throw new NotFoundException("Congestion Tax not found");
            }

            congestionTaxEntity.CityId = request.data.CityId;
            congestionTaxEntity.VehicleId = request.data.VehicleId;
            congestionTaxEntity.TaxOn = request.data.TaxOn;
            congestionTaxEntity.Amount = request.TaxAmount;

            _context.CongestionTaxes.Update(congestionTaxEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
