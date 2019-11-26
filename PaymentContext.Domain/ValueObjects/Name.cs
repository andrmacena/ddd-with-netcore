using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract()
            .Requires()
            .HasMinLen(firstName, 3, firstName, "O nome deve conter no mínimo 3 caracteres")
            .HasMinLen(lastName, 3, lastName, "O nome deve conter no mínimo 3 caracteres"));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

    }
}