using System;
using System.Collections.Generic;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Web.Common.Models;
using Ewk.BandWebsite.Web.Common.Models.Performance;

namespace Ewk.BandWebsite.Web.Common.ModelMappers
{
    public interface IPerformanceMapper
    {
        Performance Map(AddPerformanceModel model);
        Performance Map(UpdatePerformanceModel model, Guid performanceId);
        UpdatePerformanceModel MapToUpdate(Performance performance);
        PerformanceDetailsModel MapToDetail(Performance performance);
        ItemListModel<PerformanceDetailsModel> Map(IEnumerable<Performance> performance);
    }
}