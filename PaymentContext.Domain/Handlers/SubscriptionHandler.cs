using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable,
     IHandler<CreateBoletoSubscriptionCommand>,
     IHandler<CreatePayPalSubscriptionCommand>,
     IHandler<CreateCreditCardSubscriptionCommand>

    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validation
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            //Verificar se Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            //Verificar se Email já está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está em uso");

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);

            //Gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, new Document(command.PayerNumberDocument, command.PayerDocumentType), command.Payer, address, email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar informações
            _repository.CreateSubscription(student);

            //Enviar email de boas vindas
            _emailService.Send(student.ToString(), student.Email.Address, "Bem vindo", "Aqui está o seu acesso");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //Fail Fast Validation
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            //Verificar se Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            //Verificar se Email já está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está em uso");

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);

            //Gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(command.TransactionCode, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, new Document(command.PayerNumberDocument, command.PayerDocumentType), command.Payer, address, email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar informações
            _repository.CreateSubscription(student);

            //Enviar email de boas vindas
            _emailService.Send(student.ToString(), student.Email.Address, "Bem vindo", "Aqui está o seu acesso");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            //Fail Fast Validation
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            //Verificar se Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            //Verificar se Email já está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está em uso");

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);

            //Gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CrediCardPayment(command.CardHolderName, command.CardNumber, command.LastTransactionNumber, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, new Document(command.PayerNumberDocument, command.PayerDocumentType), command.Payer, address, email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar informações
            _repository.CreateSubscription(student);

            //Enviar email de boas vindas
            _emailService.Send(student.ToString(), student.Email.Address, "Bem vindo", "Aqui está o seu acesso");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }

}