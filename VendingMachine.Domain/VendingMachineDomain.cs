using System;

namespace VendingMachine.Domain
{
    public class VendingMachineDomain
    {
        public Guid Id { get; }
        public String SecretCode { get; }

        public VendingMachineDomain(Guid id, String secretCode)
        {
            Id = id;
            SecretCode = secretCode;
        }
    }
}
