using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void AdicionarAssinatura()
        {
            var subscription = new Subscription(null);
            var student = new Student(new Domain.ValueObjects.Name("Andre","Macena"), new Domain.ValueObjects.Document("12345678912",new Domain.Enums.EDocumentType()),new Domain.ValueObjects.Email("andre@teste.com"));
            student.AddSubscription(subscription);
        }
    }
}
