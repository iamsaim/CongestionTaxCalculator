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
    public class ToggleDeleteCityCommand : IRequest<bool>
    {
        public ToggleDeleteDTO data { get; set; }
    }

    public class ToggleDeleteCityHandler : IRequestHandler<ToggleDeleteCityCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ToggleDeleteCityHandler> _logger;

        public ToggleDeleteCityHandler(ApplicationDbContext context,
            ILogger<ToggleDeleteCityHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Handle(ToggleDeleteCityCommand request, CancellationToken cancellationToken)
        {
            var cityEntity = await _context.Cities.FirstOrDefaultAsync(x => x.Id == request.data.Id
                , cancellationToken);

            if (cityEntity == null)
            {
                throw new NotFoundException("City not found");
            }

            cityEntity.DeletedDate = request.data.Delete == true ? DateTime.Now : null;

            _context.Cities.Update(cityEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
