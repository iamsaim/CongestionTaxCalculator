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

namespace CongestionTaxCalculator.Application.Features.CongestionTax.Command
{
    public class ToggleDeleteCongestionTaxCommand : IRequest<bool>
    {
        public ToggleDeleteDTO data { get; set; }
    }

    public class ToggleDeleteCongestionTaxHandler : IRequestHandler<ToggleDeleteCongestionTaxCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ToggleDeleteCongestionTaxHandler> _logger;

        public ToggleDeleteCongestionTaxHandler(ApplicationDbContext context,
            ILogger<ToggleDeleteCongestionTaxHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(ToggleDeleteCongestionTaxCommand request, CancellationToken cancellationToken)
        {
            var congestionTaxEntity = await _context.CongestionTaxes.FirstOrDefaultAsync(x => x.Id == request.data.Id
                , cancellationToken);

            if (congestionTaxEntity == null)
            {
                throw new NotFoundException("Congestion Taxes not found");
            }

            congestionTaxEntity.DeletedDate = request.data.Delete == true ? DateTime.Now : null;

            _context.CongestionTaxes.Update(congestionTaxEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
