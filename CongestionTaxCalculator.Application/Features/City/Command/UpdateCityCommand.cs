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

namespace CongestionTaxCalculator.Application.Features.City.Command
{
    public class UpdateCityCommand : IRequest<bool>
    {
        public UpdateCityDTO data { get; set; }
    }

    public class UpdateTaxExemptVehicleHandler : IRequestHandler<UpdateCityCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UpdateTaxExemptVehicleHandler> _logger;

        public UpdateTaxExemptVehicleHandler(ApplicationDbContext context,
            ILogger<UpdateTaxExemptVehicleHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var cityEntity = await _context.Cities.FirstOrDefaultAsync(x => x.Id == request.data.Id
                                                                   && x.DeletedDate == null, cancellationToken);

            if (cityEntity == null)
            {
                throw new NotFoundException("City not found");
            }


            cityEntity.Name = request.data.Name;
            cityEntity.MaxTaxPerDay = request.data.MaxTaxPerDay;

            _context.Cities.Update(cityEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
