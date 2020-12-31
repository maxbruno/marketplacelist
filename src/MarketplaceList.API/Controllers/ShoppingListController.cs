using MarketplaceList.API.Services.Interfaces;
using MarketplaceList.API.ViewModels.Request;
using MarketplaceList.API.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketplaceList.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/shopping-lists")]
    public class ShoppingListController : ControllerBase
    {
        private readonly IShoppingListService _shoppingListService;
        public ShoppingListController(IShoppingListService shoppingListService)
        {
            _shoppingListService = shoppingListService;

        }


        /// <summary>
        /// Lista.
        /// </summary>
        /// <returns>Lista.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingListViewModelResponse>>> GetAll()
        {
            return Ok(await _shoppingListService.GetAllAsync());
        }

        /// <summary>
        /// Lista por por Id.
        /// </summary>
        /// <param name="list">Parâmetro "id" da lista.</param>
        /// <returns>lista.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingListItemViewModelResponse>> GetById([FromQuery] ShoppingListIdViewModelRequest list)
        {
            var listVM = await _shoppingListService.GetByIdAsync(list);

            if (listVM == null)
            {
                return NotFound();
            }

            return Ok(listVM);
        }

        /// <summary>
        /// Criação da lista.
        /// </summary>
        /// <param name="list">Parâmetro "ShoppingListViewModel".</param>
        /// <returns>list criada.</returns>
        [HttpPost]
        public ActionResult<ShoppingListViewModelResponse> PostList([FromBody] ShoppingListViewModelRequest list)
        {
            if (list == null)
            {
                return NotFound();
            }

            return Created(nameof(GetById), _shoppingListService.Add(list));
        }

        /// <summary>
        /// Exclusão da lista.
        /// </summary>
        /// <param name="list">Parâmetro "id" da lista.</param>
        /// <returns>Lista excluida.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteList([FromQuery] ShoppingListIdViewModelRequest list)
        {
            var listVM = await _shoppingListService.GetByIdAsync(list);

            if (listVM == null)
            {
                return NotFound();
            }

            _shoppingListService.Remove(listVM);

            return NoContent();
        }
    }
}