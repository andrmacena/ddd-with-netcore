using System;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CreatePayPalSubscriptionCommand: ICommand
    {
        //classe que auxilia na criação de uma subscription
        //portanto, nela contém todas as propriedades para criar uma subscription

        //student
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        //paypal
        public string TransactionCode { get; set; }
        //payment
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string PayerNumberDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
        public string Payer { get; set; }
        public string PayerEmail { get; set; }
        //Address
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }

}