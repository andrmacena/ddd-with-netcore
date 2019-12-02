using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Student _student;
        private readonly Subscription _subscription;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Name _name;
        private readonly Email _email;
        public StudentTests()
        {
            _name = new Name("Jose", "Fino");
            _address = new Address("Rua", "75", "Jardim", "Itu", "SP", "Brasil", "13306981");
            _document = new Document("12345678912", EDocumentType.CPF);
            _email = new Email("josefino@teste.com");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(DateTime.Now.AddDays(10));
        }

        [TestMethod]
        public void ShouldReturnErrorWhenAddAnotherSubscriptionWithAnActiveSubscription()
        {
            var payment = new PayPalPayment("12345", DateTime.Now, DateTime.Now.AddDays(3), 100, 100, _document, "André", _address, _email);
            _subscription.AddPayment(payment);

            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenActiveSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var payment = new PayPalPayment("12345", DateTime.Now, DateTime.Now.AddDays(3), 100, 100, _document, "André", _address, _email);
            _subscription.AddPayment(payment);

            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Valid);

        }
    }
}
