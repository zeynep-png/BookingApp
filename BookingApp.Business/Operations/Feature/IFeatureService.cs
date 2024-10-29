using BookingApp.Business.Operations.Feature.Dtos;
using BookingApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.Feature
{
    // Interface defining feature-related operations for BookingApp
    public interface IFeatureService
    {
        // Method to add a new feature, taking a DTO as input and returning a service message
        Task<ServiceMessage> AddFeature(AddFeatureDto feature);
    }
}
