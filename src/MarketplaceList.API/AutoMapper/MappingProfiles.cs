using AutoMapper;
using MarketplaceList.API.ViewModels.Request;
using MarketplaceList.API.ViewModels.Response;
using MarketplaceList.Domain.Models;

namespace MarketplaceList.API.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region ShoppingList
            CreateMap<ShoppingList, ShoppingListViewModelResponse>().ReverseMap();
            CreateMap<ShoppingList, ShoppingListViewModelRequest>().ReverseMap();
            CreateMap<ShoppingList, ShoppingListItemViewModelResponse>().ReverseMap();
            #endregion
            #region Item
            CreateMap<Item, ItemViewModelResponse>().ReverseMap();
            CreateMap<Item, ItemViewModelRequest>().ReverseMap();
            CreateMap<Item, ItemIdViewModelRequest>().ReverseMap();
            CreateMap<Item, ItemQtdViewModelRequest>().ReverseMap();

            //CreateMap<ItemQtdViewModelRequest, Item>()
            //      .ConstructUsing(x => new Item(x.Id, x.Qtd));
            #endregion
        }
    }
}