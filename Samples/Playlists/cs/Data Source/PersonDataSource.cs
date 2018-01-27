
using HyperStoreServiceAPP.DTO;
using Models;
using Newtonsoft.Json;
using SDKTemplate.CustomeModel;
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
        public static async Task<Person> CreateNewPerson(SupplierDTO supplierDTO)
        {
            var person = await Utility.CreateAsync<Person>(BaseURI.HyperStoreService + API.Persons, supplierDTO);
            if (person != null)
            {
                string message;
                if (person.EntityType == EntityType.Supplier)
                    message = String.Format("You can start placing orders to Supplier {0} ({1})", person.Name, person.MobileNo);
                else
                    message = String.Format("You can start taking orders from Customer {0} ({1})", person.Name, person.MobileNo);

                PersonCreatedEvent?.Invoke();
                SuccessNotification.PopUpHttpPostSuccessNotification(API.Persons, message);
            }
            return person;
        }
        #endregion

        #region Update
        public static async Task<Person> UpdateSupplierAsync(Guid supplierId, SupplierDTO supplierDTO)
        {
            var supplier = await Utility.UpdateAsync<Person>(BaseURI.HyperStoreService + API.Persons, supplierId.ToString(), supplierDTO);
            if (supplier != null)
            {
                //TODO: Success Notification
                PersonUpdatedEvent?.Invoke();
            }
            return supplier;
        }

        #endregion

        #region Read
        public static async Task<List<Person>> RetrievePersonsAsync(SupplierFilterCriteriaDTO sfc)
        {
            List<Person> suppliers = await Utility.RetrieveAsync<List<Person>>(BaseURI.HyperStoreService + API.Persons, null, sfc);
            return suppliers;
        }

        public static async Task<Person> RetrieveThePersonAsync(Guid supplierId)
        {
            Person supplier = await Utility.RetrieveAsync<Person>(BaseURI.HyperStoreService + API.Persons, supplierId.ToString(), null);
            return supplier;
        }

        public static async Task<PersonMetadata> RetrievePersonMetadata(PersonMetadataDTO personMetadataDTO)
        {
            var personMetadata = await Utility.RetrieveAsync<PersonMetadata>(BaseURI.HyperStoreService + API.Persons, CustomAction.GetPersonMetadata, personMetadataDTO);
            return personMetadata;
        }

        
        #endregion 
    }
}
