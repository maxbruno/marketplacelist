using System;

namespace MarketplaceList.API.ViewModels.Request
{
    public class ItemViewModelRequest
    {
        public string Name { get; set; }
        public int Qtd { get; set; }
        public Guid ShoppingListId { get; set; }
    }
}
