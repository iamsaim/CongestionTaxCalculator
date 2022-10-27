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
    public class UpdateTaxScheduleCommand : IRequest<bool>
    {
        public UpdateTaxScheduleDTO data { get; set; }
    }

    public class UpdateTaxScheduleCHandler : IRequestHandler<UpdateTaxScheduleCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UpdateTaxScheduleCHandler> _logger;

        public UpdateTaxScheduleCHandler(ApplicationDbContext context,
            ILogger<UpdateTaxScheduleCHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(UpdateTaxScheduleCommand request, CancellationToken cancellationToken)
        {
            var isCityExists = await _context.Cities.AnyAsync(x => x.Id == request.data.CityId
                                                                   && x.DeletedDate == null, cancellationToken);

            if (!isCityExists)
            {
                throw new NotFoundException("City not found");
            }

            var taxScheduleEntity = await _context.TaxSchedules.FirstOrDefaultAsync(x => x.Id == request.data.Id
                && x.DeletedDate == null, cancellationToken);

            if (taxScheduleEntity == null)
            {
                throw new NotFoundException("Tax Schedule not found");
            }

            taxScheduleEntity.CityId = request.data.CityId;
            taxScheduleEntity.StartTime = request.data.StartTime;
            taxScheduleEntity.EndTime = request.data.EndTime;
            taxScheduleEntity.Amount = request.data.Amount;

            _context.TaxSchedules.Update(taxScheduleEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
