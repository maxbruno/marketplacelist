using MarketplaceList.API.ViewModels.Request;
using MarketplaceList.API.ViewModels.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketplaceList.API.Services.Interfaces
{
    public interface IItemService
    {
        Task<ItemViewModelResponse> GetByIdAsync(ItemIdViewModelRequest itemVM);
        Task<IEnumerable<ItemViewModelResponse>> GetAllAsync(ShoppingListIdViewModelRequest shoppingListVM);
        IEnumerable<ItemViewModelResponse> Add(IEnumerable<ItemViewModelRequest> itemVM);
        void Remove(ItemViewModelResponse itemVM);
        void EditQtd(ItemQtdViewModelRequest itemVM);
    }
}