using SDKTemplate;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Models
{
    public class Person : IPerson
    {
        [Required]
        public EntityType? EntityType { get; set; }
        public Guid PersonId { get; set; }
        public string Address { get; set; }
        public string GSTIN { get; set; }

        [Required]
        [StringLength(10)]
        public string MobileNo { get; set; }

        [Required]
        [StringLength(24)]
        public string Name { get; set; }
        public decimal WalletBalance { get; set; }
        public decimal? NetWorth { get; set; }
        public DateTime FirstVisited { get; set; }
        public DateTime LastVisited { get; set; }
        public DateTime? LastCalled { get; set; }
        [Range(0, 5)]
        public int? Rating { get; set; }
        public DateTime? PreferedTimeToContact { get; set; }
        public Person()
        {
        }
    }

    public interface IPerson
    {
        string Address { get; set; }
        string GSTIN { get; set; }
        [Required]
        string MobileNo { get; set; }
        [Required]
        string Name { get; set; }
        decimal WalletBalance { get; set; }
    }
}