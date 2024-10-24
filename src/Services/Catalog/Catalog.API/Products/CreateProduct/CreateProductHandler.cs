
namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(r=>r.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(r => r.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(r => r.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(r => r.Price).GreaterThan(0).WithMessage("Price should be greater than 0");

    }
}
internal class CreateProductCommandHandler(IDocumentSession session,IValidator<CreateProductCommand> validator)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //create Product entity from command object
        //save to database
        //return CreateProductResult result
        var result = await validator.ValidateAsync(command, cancellationToken);
        var errors = result.Errors.Select(r => r.ErrorMessage).ToList();
        if (errors.Any())
        {
            throw new ValidationException(errors.FirstOrDefault());
        }
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };
        
        //save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        //return result
        return new CreateProductResult(product.Id);
    }
}
