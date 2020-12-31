using AutoMapper;
using MarketplaceList.API.ViewModels.Request;
using MarketplaceList.API.ViewModels.Response;
using MarketplaceList.Domain.Interfaces.Notifications;
using MarketplaceList.Domain.Interfaces.Repository;
using MarketplaceList.Domain.Interfaces.UoW;
using MarketplaceList.Domain.Models;
using MarketplaceList.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketplaceList.API.Services.Interfaces
{
    public class ItemService : IItemService
    {
        private readonly IDomainNotification _domainNotification;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(IItemRepository itemRepository,
                                   IDomainNotification domainNotification,
                                   IMapper mapper,
                                   IUnitOfWork unitOfWork)
        {
            _domainNotification = domainNotification;
            _itemRepository = itemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ItemViewModelResponse> GetByIdAsync(ItemIdViewModelRequest itemVM)
        {
            return _mapper.Map<ItemViewModelResponse>(await _itemRepository.GetByIdAsync(itemVM.Id));

        }
        public IEnumerable<ItemViewModelResponse> Add(IEnumerable<ItemViewModelRequest> itemVM)
        {
            List<ItemViewModelResponse> viewModel = new List<ItemViewModelResponse>();
            foreach (var item in itemVM)
            {
                var model = _mapper.Map<Item>(item);
                var validation = new ItemInsertValidation().Validate(model);

                if (!validation.IsValid)
                {
                    _domainNotification.AddNotifications(validation);
                    return viewModel;
                }

                _itemRepository.Add(model);

                var vm = _mapper.Map<ItemViewModelResponse>(model);
                viewModel.Add(vm);
            }

            _unitOfWork.Commit();

            return viewModel;
        }
        public void Remove(ItemViewModelResponse itemVM)
        {
            var model = _mapper.Map<Item>(itemVM);

            var validation = new ItemDeleteValidation().Validate(model);

            if (!validation.IsValid)
            {
                _domainNotification.AddNotifications(validation);
                return;
            }

            _itemRepository.Remove(model);
            _unitOfWork.Commit();
        }
        public async Task<IEnumerable<ItemViewModelResponse>> GetAllAsync(ShoppingListIdViewModelRequest shoppingListVM)
        {
            return _mapper.Map<IEnumerable<ItemViewModelResponse>>(await _itemRepository.GetAllAsync(shoppingListVM.Id));
        }

        public void EditQtd(ItemQtdViewModelRequest itemVM)
        {
            var item = GetByIdAsync(new ItemIdViewModelRequest(itemVM.Id)).Result;
            var model = new Item(item.Id, item.Name, itemVM.Qtd, item.ShoppingListId);

            var validation = new ItemDeleteValidation().Validate(model);

            if (!validation.IsValid)
            {
                _domainNotification.AddNotifications(validation);
                return;
            }

            _itemRepository.Update(model);
            _unitOfWork.Commit();
        }
    }
}