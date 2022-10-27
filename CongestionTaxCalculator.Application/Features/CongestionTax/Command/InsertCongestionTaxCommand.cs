using MediatR;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Exceptions;
using CongestionTaxCalculator.Domain.Entities;
using CongestionTaxCalculator.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CongestionTaxCalculator.Application.Features.CongestionTax.Command
{
    public class InsertCongestionTaxCommand : IRequest<bool>
    {
        public InsertCongestionTaxDTO data { get; set; }
        public double TaxAmount { get; set; }
    }

    public class InsertCongestionTaxHandler : IRequestHandler<InsertCongestionTaxCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InsertCongestionTaxHandler> _logger;

        public InsertCongestionTaxHandler(ApplicationDbContext context,
            ILogger<InsertCongestionTaxHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(InsertCongestionTaxCommand request, CancellationToken cancellationToken)
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

            var congestionTaxEntity = new CongestionTaxEntity()
            {
                CityId = request.data.CityId,
                VehicleId = request.data.VehicleId,
                TaxOn = request.data.TaxOn,
                Amount = request.TaxAmount
            };

            var isAdded = await _context.CongestionTaxes.AddAsync(congestionTaxEntity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return isAdded.State == EntityState.Unchanged;
        }

    }
}
