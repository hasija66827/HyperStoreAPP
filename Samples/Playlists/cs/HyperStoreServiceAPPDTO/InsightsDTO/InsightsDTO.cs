using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Models;
using SDKTemplate.DTO;
using SDKTemplate;

namespace HyperStoreServiceAPP.DTO.InsightsDTO
{
    public class InsightsDTO
    {
        private IRange<DateTime> _dateRange;
        [Required]
        [DateRange(ErrorMessage = "{0} is invalid, lb>ub")]
        public IRange<DateTime> DateRange { get { return this._dateRange; } }

        public InsightsDTO(IRange<DateTime> dateRange)
        {
            _dateRange = dateRange;
        }
    }

    public class FiniteInsightsDTO : InsightsDTO
    {
        private uint? _numberOfRecords;
        [Required]
        public uint? NumberOfRecords { get { return this._numberOfRecords; } }

        public FiniteInsightsDTO(IRange<DateTime> dateRange, uint numberOfRecords) : base(dateRange)
        {
            _numberOfRecords = numberOfRecords;
        }
    }
}