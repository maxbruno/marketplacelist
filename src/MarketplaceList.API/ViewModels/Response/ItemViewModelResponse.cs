using System;

namespace MarketplaceList.API.ViewModels.Response
{
    public class ItemViewModelResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Qtd { get; set; }
        public Guid ShoppingListId { get; set; }
    }
}
