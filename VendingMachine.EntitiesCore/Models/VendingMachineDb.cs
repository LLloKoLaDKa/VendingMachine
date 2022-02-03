using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.EntitiesCore.Models
{
    [Table("VendingMachines")]
    public class VendingMachineDb
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("secretcode")]
        public String SecretCode { get; set; }

        public VendingMachineDb(Guid id, String secretCode)
        {
            Id = id;
            SecretCode = secretCode;
        }
    }
}
