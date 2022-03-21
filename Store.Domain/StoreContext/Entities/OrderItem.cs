using Flunt.Validations;
using Store.Shared.Entities;

namespace Store.StoreDomain.StoreContext.Entities;

public class OrderItem : Entity
{
    public OrderItem(Product product, decimal quantity, decimal price)
    {
        Product = product;
        Quantity = quantity;
        Price = price;
    }

    public Product Product { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal Price { get; private set; }

    public override void Validate()
    {
        AddNotifications(new Contract()
            .Requires()
            .IsTrue(Product is not null, "Product", "Produto Inválido")
            .IsTrue(Quantity > 0, "Product.Quantity", "Quantidade Inválida")
            .IsTrue(Price > 0, "Product.Price", "Preço Inválido")
            .IsTrue(Product is not null && Product.QuantityOnHand >= Quantity, "Product.QuantityOnHand", "Quantidade do produto insuficiente")
        );
    }
}
