using MarketplaceList.API.ViewModels.Request;
using MarketplaceList.API.ViewModels.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketplaceList.API.Services.Interfaces
{
    public interface IShoppingListService
    {
        Task<ShoppingListItemViewModelResponse> GetByIdAsync(ShoppingListIdViewModelRequest shoppingListVM);
        Task<IEnumerable<ShoppingListViewModelResponse>> GetAllAsync();
        ShoppingListViewModelResponse Add(ShoppingListViewModelRequest shoppingListVM);
        void Remove(ShoppingListItemViewModelResponse shoppingListVM);
    }
}