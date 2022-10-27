using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Exceptions;
using CongestionTaxCalculator.Application.Features.TaxExemptVehicle.Command;
using CongestionTaxCalculator.Application.Features.TaxFreeDay.Command;
using CongestionTaxCalculator.Domain.Entities;
using CongestionTaxCalculator.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.CompilerServices;

namespace CongestionTaxCalculator.Application.Features.TaxSchedule.Command
{
    public class InsertTaxScheduleCommand : IRequest<bool>
    {
        public InsertTaxScheduleDTO data { get; set; }
    }

    public class InsertTaxScheduleHandler : IRequestHandler<InsertTaxScheduleCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InsertTaxScheduleHandler> _logger;

        public InsertTaxScheduleHandler(ApplicationDbContext context,
            ILogger<InsertTaxScheduleHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(InsertTaxScheduleCommand request, CancellationToken cancellationToken)
        {
            var isCityExists = await _context.Cities.AnyAsync(x => x.Id == request.data.CityId
                                                                   && x.DeletedDate == null, cancellationToken);

            if (!isCityExists)
            {
                throw new NotFoundException("City not found");
            }

            var scheduleEntity = new TaxScheduleEntity()
            {
                CityId = request.data.CityId,
                StartTime = request.data.StartTime,
                EndTime = request.data.EndTime,
                Amount = request.data.Amount
            };

            var isAdded = await _context.TaxSchedules.AddAsync(scheduleEntity, cancellationToken);


            await _context.SaveChangesAsync(cancellationToken);

            return isAdded.State == EntityState.Unchanged;
        }

    }
}
