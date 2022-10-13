using ITExpertTask.Data;
using ITExpertTask.Data.Entities;
using ITExpertTask.Models;
using ITExpertTask.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ITExpertTask.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<IEnumerable<GetItemModel>> GetItems([FromBody] IEnumerable<DataFilter> filters)
        {
            return await _itemService.GetItemsAsync(filters);
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> AddItems([FromBody] KeyValuePair<int, string>[] items)
        {
            var addModels = items.Select(i => new AddItemModel
            {
                Code = i.Key,
                Value = i.Value
            }).ToList();

            await _itemService.AddItemsAsync(addModels);

            return Ok();
        }
       
    }
}

