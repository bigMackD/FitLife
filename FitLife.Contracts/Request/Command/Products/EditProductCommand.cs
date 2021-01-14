using FitLife.Shared.Infrastructure.Command;

namespace FitLife.Contracts.Request.Command.Products
{
    public class EditProductCommand : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal ProteinsGrams { get; set; }
        public decimal CarbsGrams { get; set; }
        public decimal FatsGrams { get; set; }
    }
}
