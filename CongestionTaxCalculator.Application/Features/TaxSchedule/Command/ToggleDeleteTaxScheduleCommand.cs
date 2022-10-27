using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Exceptions;
using CongestionTaxCalculator.Application.Features.TaxExemptVehicle.Command;
using CongestionTaxCalculator.Application.Features.TaxFreeDay.Command;
using CongestionTaxCalculator.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CongestionTaxCalculator.Application.Features.TaxSchedule.Command
{
    public class ToggleDeleteTaxScheduleCommand : IRequest<bool>
    {
        public ToggleDeleteDTO data { get; set; }
    }

    public class ToggleDeleteTaxScheduleHandler : IRequestHandler<ToggleDeleteTaxScheduleCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ToggleDeleteTaxScheduleHandler> _logger;

        public ToggleDeleteTaxScheduleHandler(ApplicationDbContext context,
            ILogger<ToggleDeleteTaxScheduleHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(ToggleDeleteTaxScheduleCommand request, CancellationToken cancellationToken)
        {
            var scheduleEntity = await _context.TaxSchedules.FirstOrDefaultAsync(x => x.Id == request.data.Id
                , cancellationToken);

            if (scheduleEntity == null)
            {
                throw new NotFoundException("Tax Schedule not found");
            }

            scheduleEntity.DeletedDate = request.data.Delete == true ? DateTime.Now : null;

            _context.TaxSchedules.Update(scheduleEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
