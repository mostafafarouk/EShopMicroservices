
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeletebasketReult>;
    public record DeletebasketReult(bool IsSuccess);

    public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is  required");
        }
    }
    public class DeleteBasketCommandHandler(IBasketRepository repository) :
        ICommandHandler<DeleteBasketCommand, DeletebasketReult>
    {
        public async Task<DeletebasketReult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteBasket(command.UserName, cancellationToken);
            return new DeletebasketReult(true);
        }
    }
}
