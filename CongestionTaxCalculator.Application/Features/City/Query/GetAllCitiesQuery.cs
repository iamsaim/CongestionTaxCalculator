using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Exceptions;
using CongestionTaxCalculator.Application.Features.TaxExemptVehicle.Query;
using CongestionTaxCalculator.Domain.Entities;
using CongestionTaxCalculator.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.CompilerServices;

namespace CongestionTaxCalculator.Application.Features.City.Query
{ 
    public class GetAllCitiesQuery : IRequest<List<GetAllCitiesDTO>>
    {
    }

    public class GetAllCitiesHandler : IRequestHandler<GetAllCitiesQuery, List<GetAllCitiesDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GetAllCitiesHandler> _logger;

        public GetAllCitiesHandler(ApplicationDbContext context,
            ILogger<GetAllCitiesHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<GetAllCitiesDTO>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var listAsync = await _context.Cities
                .Select(x => new GetAllCitiesDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    MaxTaxPerDay = x.MaxTaxPerDay

                })
                .ToListAsync(cancellationToken);

            return listAsync;


        }
    }
}
