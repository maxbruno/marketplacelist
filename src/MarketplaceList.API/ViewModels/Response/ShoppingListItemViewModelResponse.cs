using System;
using System.Collections.Generic;

namespace MarketplaceList.API.ViewModels.Response
{
    public class ShoppingListItemViewModelResponse
    {
        public ShoppingListItemViewModelResponse()
        {
            Itens = new List<ItemViewModelResponse>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<ItemViewModelResponse> Itens { get; set; }
    }
}