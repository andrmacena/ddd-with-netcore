using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract().Requires().IsEmail(Address, Address, "Email inválido"));
        }

        public string Address { get; private set; }

    }
}