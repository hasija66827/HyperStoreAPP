using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using SDKTemplate;

namespace HyperStoreServiceAPP.DTO.InsightsDTO
{
    public class CustomerInsightsDTO : FiniteInsightsDTO
    {
        public CustomerInsightsDTO(IRange<DateTime> dateRange, uint numberOfRecords) : base(dateRange, numberOfRecords) { }
    }

    public class CustomerInsights
    {
        List<Person> _Customer;
        public List<Person> Customer { get { return this._Customer; } }

        public CustomerInsights(List<Person> customers)
        {
            _Customer = customers;
        }
    }

    public class DetachedCustomerInsights : CustomerInsights
    {
        int _detachedCustomerCount;
        public int DetachedCustomerCount { get { return this._detachedCustomerCount; } }

        public DetachedCustomerInsights(int detachedCustomerCount, List<Person> customer) : base(customer)
        {
            _detachedCustomerCount = detachedCustomerCount;
        }
    }

    public class NewCustomerInsights : CustomerInsights
    {
        int _newCustomerCount;
        public int NewCustomerCount { get { return this._newCustomerCount; } }

        public NewCustomerInsights(int newCustomerCount, List<Person> customer) : base(customer)
        {
            _newCustomerCount = newCustomerCount;
        }
    }
}