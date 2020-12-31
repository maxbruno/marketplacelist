using AutoMapper;
using MarketplaceList.API.ViewModels.Request;
using MarketplaceList.API.ViewModels.Response;
using MarketplaceList.Domain.Models;
using MarketplaceList.Domain.Interfaces.Notifications;
using MarketplaceList.Domain.Interfaces.Repository;
using MarketplaceList.Domain.Interfaces.UoW;
using MarketplaceList.Domain.Validations;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MarketplaceList.API.Services.Interfaces
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly IDomainNotification _domainNotification;
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingListService(IShoppingListRepository shoppingListRepository,
                                   IDomainNotification domainNotification,
                                   IMapper mapper,
                                   IUnitOfWork unitOfWork)
        {
            _domainNotification = domainNotification;
            _shoppingListRepository = shoppingListRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public ShoppingListViewModelResponse Add(ShoppingListViewModelRequest shoppingListVM)
        {
            ShoppingListViewModelResponse viewModel = null;
            var model = _mapper.Map<ShoppingList>(shoppingListVM);

            var validation = new ShoppingListInsertValidation().Validate(model);

            if (!validation.IsValid)
            {
                _domainNotification.AddNotifications(validation);
                return viewModel;
            }

            _shoppingListRepository.Add(model);
            _unitOfWork.Commit();

            viewModel = _mapper.Map<ShoppingListViewModelResponse>(model);

            return viewModel;
        }
        public void Remove(ShoppingListItemViewModelResponse shoppingListVM)
        {
            var model = _mapper.Map<ShoppingList>(shoppingListVM);

            var validation = new ShoppingListDeleteValidation().Validate(model);

            if (!validation.IsValid)
            {
                _domainNotification.AddNotifications(validation);
                return;
            }

            _shoppingListRepository.Remove(model);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<ShoppingListViewModelResponse>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<ShoppingListViewModelResponse>>(await _shoppingListRepository.GetAllAsync());

        }

        public async Task<ShoppingListItemViewModelResponse> GetByIdAsync(ShoppingListIdViewModelRequest shoppingListVM)
        {
            return _mapper.Map<ShoppingListItemViewModelResponse>(await _shoppingListRepository.GetByIdAsync(shoppingListVM.Id));
        }
    }
}