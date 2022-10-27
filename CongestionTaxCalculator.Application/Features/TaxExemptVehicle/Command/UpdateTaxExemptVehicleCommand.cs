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
    public class UpdateTaxExemptVehicleCommand : IRequest<bool>
    {
        public UpdateTaxExemptVehicleDTO data { get; set; }
    }

    public class UpdateTaxExemptVehicleHandler : IRequestHandler<UpdateTaxExemptVehicleCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UpdateTaxExemptVehicleHandler> _logger;

        public UpdateTaxExemptVehicleHandler(ApplicationDbContext context,
            ILogger<UpdateTaxExemptVehicleHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(UpdateTaxExemptVehicleCommand request, CancellationToken cancellationToken)
        {
            var isCityExists = await _context.Cities.AnyAsync(x => x.Id == request.data.CityId
                                                                   && x.DeletedDate == null, cancellationToken);

            if (!isCityExists)
            {
                throw new NotFoundException("City not found");
            }

            var taxExemptVehicleEntity = await _context.TaxExemptVehicles.FirstOrDefaultAsync(x => x.Id == request.data.Id
                                                                   && x.DeletedDate == null, cancellationToken);

            if (taxExemptVehicleEntity == null)
            {
                throw new NotFoundException("Tax Exempt Vehicle not found");
            }

            taxExemptVehicleEntity.CityId = request.data.CityId;
            taxExemptVehicleEntity.Type = request.data.Type;
            
            _context.TaxExemptVehicles.Update(taxExemptVehicleEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
