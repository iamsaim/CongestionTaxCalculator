using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Exceptions;
using CongestionTaxCalculator.Application.Features.TaxExemptVehicle.Command;
using CongestionTaxCalculator.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CongestionTaxCalculator.Application.Features.TaxFreeDay.Command
{
    public class ToggleDeleteTaxFreeDayCommand : IRequest<bool>
    {
        public ToggleDeleteDTO data { get; set; }
    }

    public class ToggleDeleteTaxFreeDayHandler : IRequestHandler<ToggleDeleteTaxFreeDayCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ToggleDeleteTaxFreeDayHandler> _logger;

        public ToggleDeleteTaxFreeDayHandler(ApplicationDbContext context,
            ILogger<ToggleDeleteTaxFreeDayHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(ToggleDeleteTaxFreeDayCommand request, CancellationToken cancellationToken)
        {
            var taxFreeDayEntity = await _context.TaxFreeDays.FirstOrDefaultAsync(x => x.Id == request.data.Id
                , cancellationToken);

            if (taxFreeDayEntity == null)
            {
                throw new NotFoundException("Tax Free day not found");
            }

            taxFreeDayEntity.DeletedDate = request.data.Delete == true ? DateTime.Now : null;

            _context.TaxFreeDays.Update(taxFreeDayEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
