﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Domain.Constants;

namespace CongestionTaxCalculator.Application.Common.Dto
{
    public class UpdateTaxExemptVehicleDTO
    {
        public Guid Id { get; set; }
        public VehicleType Type { get; set; }
        public Guid CityId { get; set; }
    }
}
