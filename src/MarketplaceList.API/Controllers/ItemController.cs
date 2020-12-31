using MarketplaceList.API.Services.Interfaces;
using MarketplaceList.API.ViewModels.Request;
using MarketplaceList.API.ViewModels.Response;
using MarketplaceList.Domain.Interfaces.Repository;
using MarketplaceList.Domain.Interfaces.Services;
using MarketplaceList.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketplaceList.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/itens")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ITacoService _tacoService;
        public ItemController(IItemService itemService, ITacoService tacoService)
        {
            _itemService = itemService;
            _tacoService = tacoService;
        }

        /// <summary>
        /// Lista por ShoppingListId.
        /// </summary>
        /// <param name="list">Parâmetro "id" da lista.</param>
        /// <returns>lista.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ItemViewModelResponse>>> GetAll([FromQuery] ShoppingListIdViewModelRequest list)
        {
            return Ok(await _itemService.GetAllAsync(list));
        }

        /// <summary>
        /// Adicionar os itens.
        /// </summary>
        /// <param name="list">Parâmetro "ItemViewModelRequest".</param>
        /// <returns>itens.</returns>
        [HttpPost]
        public ActionResult<ItemViewModelResponse> PostItem([FromBody] List<ItemViewModelRequest> list)
        {
            if (list == null)
            {
                return NotFound();
            }

            return Created(nameof(GetAll), _itemService.Add(list));
        }

        /// <summary>
        /// Exclusão de um item da lista.
        /// </summary>
        /// <param name="list">Parâmetro "id" da lista.</param>
        /// <returns>Lista excluida.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem([FromQuery] ItemIdViewModelRequest item)
        {
            var listVM = await _itemService.GetByIdAsync(item);

            if (listVM == null)
            {
                return NotFound();
            }

            _itemService.Remove(listVM);

            return NoContent();
        }

        /// <summary>
        /// Atualização da quantidade.
        /// </summary>
        /// <param name="id">Parâmetro "id" do item.</param>
        /// <param name="item">Parâmetro "item".</param>
        /// <returns>Item atualizado.</returns>
        [HttpPatch("{id}")]
        public async Task<ActionResult> PutItem(Guid id, [FromBody] ItemQtdViewModelRequest item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var itemVM = await _itemService.GetByIdAsync(new ItemIdViewModelRequest(id));

            if (itemVM == null)
            {
                return NotFound();
            }

            _itemService.EditQtd(item);

            return NoContent();
        }

        /// <summary>
        /// Lista por Taco API.
        /// </summary>
        /// <returns>lista.</returns>
        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Taco>>> GetAllProducts()
        {
            return Ok(await _tacoService.GetAllProductsTacoAsync());
        }

    }
}