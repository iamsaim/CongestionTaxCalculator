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

namespace CongestionTaxCalculator.Application.Features.City.Command
{
    public class InsertCityCommand : IRequest<bool>
    {
        public InsertCityDTO data { get; set; }
    }

    public class InsertCityHandler : IRequestHandler<InsertCityCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InsertCityHandler> _logger;

        public InsertCityHandler(ApplicationDbContext context,
            ILogger<InsertCityHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(InsertCityCommand request, CancellationToken cancellationToken)
        {

            var cityEntity = new CityEntity()
            {
                Name = request.data.Name,
                MaxTaxPerDay = request.data.MaxTaxPerDay
            };

            var isAdded = await _context.Cities.AddAsync(cityEntity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return isAdded.State == EntityState.Unchanged;
        }

    }
}
