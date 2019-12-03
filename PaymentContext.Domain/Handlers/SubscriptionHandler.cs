using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Repositories;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>
    {

        private readonly IStudentRepository _repository;

        public SubscriptionHandler(IStudentRepository repository)
        {
            _repository = repository;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Verificar se Documento já está cadastrado

            //Verificar se Email já está cadastrado

            //erar os VOs

            //Gerar as entidades

            //Aplicar as validações

            //Salvar informações

            //Enviar email de boas vindas

            //Retornar informações

            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }

}