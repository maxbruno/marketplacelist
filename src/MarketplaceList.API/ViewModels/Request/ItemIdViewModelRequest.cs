using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceList.API.ViewModels.Request
{
    public class ItemIdViewModelRequest
    {
        public ItemIdViewModelRequest() { }

        public ItemIdViewModelRequest(Guid id)
        {
            Id = id;
        }

        [FromRoute(Name = "id")]
        [Required(ErrorMessage = "Id é obrigatório")]
        public Guid Id { get; set; }
    }
}