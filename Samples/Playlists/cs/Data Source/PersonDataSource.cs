
using Models;
using Newtonsoft.Json;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public delegate void PersonEntityChangedDelegate();
    class PersonDataSource
    {
        public static event PersonEntityChangedDelegate PersonCreatedEvent;
        public static event PersonEntityChangedDelegate PersonUpdatedEvent;

        #region Create
        public static async Task<TSupplier> CreateNewPerson(SupplierDTO supplierDTO)
        {
            var person = await Utility.CreateAsync<TSupplier>(BaseURI.HyperStoreService + API.Persons, supplierDTO);
            if (person != null)
            {
                var message = String.Format("You can Start placing orders to Supplier {0} ({1})", person.Name, person.MobileNo);
                PersonCreatedEvent?.Invoke();
                SuccessNotification.PopUpHttpPostSuccessNotification(API.Persons, message);
            }
            return person;
        }
        #endregion

        #region Update
        public static async Task<TSupplier> UpdateSupplierAsync(Guid supplierId, SupplierDTO supplierDTO)
        {
            var supplier = await Utility.UpdateAsync<TSupplier>(BaseURI.HyperStoreService + API.Persons, supplierId.ToString(), supplierDTO);
            if (supplier != null)
            {
                //TODO: Success Notification
                PersonUpdatedEvent?.Invoke();
            }
            return supplier;
        }

        #endregion

        #region Read
        public static async Task<List<TSupplier>> RetrievePersonsAsync(SupplierFilterCriteriaDTO sfc)
        {
            List<TSupplier> suppliers = await Utility.RetrieveAsync<List<TSupplier>>(BaseURI.HyperStoreService + API.Persons, null, sfc);
            return suppliers;
        }

        public static async Task<TSupplier> RetrieveThePersonAsync(Guid supplierId)
        {
            TSupplier supplier = await Utility.RetrieveAsync<TSupplier>(BaseURI.HyperStoreService + API.Persons, supplierId.ToString(), null);
            return supplier;
        }

        public static async Task<Int32> RetrieveTotalPersons()
        {
            Int32 totalSuppliers = await Utility.RetrieveAsync<Int32>(BaseURI.HyperStoreService + API.Persons, CustomAction.GetTotalRecordsCount, null);
            return totalSuppliers;
        }

        public static async Task<IRange<T>> RetrieveWalletRangeAsync<T>()
        {
            var IRange = await Utility.RetrieveAsync<IRange<T>>(BaseURI.HyperStoreService + API.Persons, CustomAction.GetWalletBalanceRange, null);
            return IRange;
        }
        #endregion 
    }
}
