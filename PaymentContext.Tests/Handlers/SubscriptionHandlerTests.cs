using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {

        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "André";
            command.LastName = "Macena";
            command.Document = "99999999999";
            command.Email = "andre@gmail.com";
            //boleto
            command.BarCode = "123456789";
            command.BoletoNumber = "123456789";
            //payment
            command.PaymentNumber = "12345";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 100;
            command.TotalPaid = 100;
            command.PayerNumberDocument = "99999999999";
            command.PayerDocumentType = EDocumentType.CPF;
            command.Payer = "Andre corp";
            command.PayerEmail = "andre@gmail.com";
            //Address
            command.Street = "Rua dos Andradas";
            command.Number = "75";
            command.Neighborhood = "Bairro Brasil";
            command.City = "Campinas";
            command.State = "MG";
            command.Country = "BR";
            command.ZipCode = "13536827";

            handler.Handle(command);

            Assert.AreEqual(false, handler.Valid);
        }

    }


}