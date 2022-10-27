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
    public class ToggleDeleteTaxExemptVehicleCommand : IRequest<bool>
    {
        public ToggleDeleteDTO data { get; set; }
    }

    public class ToggleDeleteTaxExemptVehicleHandler : IRequestHandler<ToggleDeleteTaxExemptVehicleCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ToggleDeleteTaxExemptVehicleHandler> _logger;

        public ToggleDeleteTaxExemptVehicleHandler(ApplicationDbContext context,
            ILogger<ToggleDeleteTaxExemptVehicleHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(ToggleDeleteTaxExemptVehicleCommand request, CancellationToken cancellationToken)
        {
            var taxExemptVehicleEntity = await _context.TaxExemptVehicles.FirstOrDefaultAsync(x => x.Id == request.data.Id
                , cancellationToken);

            if (taxExemptVehicleEntity == null)
            {
                throw new NotFoundException("Tax Exempt Vehicle not found");
            }

            taxExemptVehicleEntity.DeletedDate = request.data.Delete == true? DateTime.Now : null;
            
            _context.TaxExemptVehicles.Update(taxExemptVehicleEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
