using ITExpertTask.Data;
using ITExpertTask.Data.Entities;
using ITExpertTask.Extensions;
using ITExpertTask.Models;
using Microsoft.EntityFrameworkCore;

namespace ITExpertTask.Services
{
    public interface IItemService
    {
        Task AddItemsAsync(IEnumerable<AddItemModel> items);
        Task<IEnumerable<GetItemModel>> GetItemsAsync(IEnumerable<DataFilter> filters);
    }
    public class ItemService : IItemService
    {
        private readonly DataContext _dataContext;
        public ItemService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddItemsAsync(IEnumerable<AddItemModel> items)
        {
            _dataContext.Items.RemoveRange(_dataContext.Items);
            await  _dataContext.SaveChangesAsync();

            await _dataContext.Items.AddRangeAsync(items.Select(i => new Item
            {
                Code = i.Code,
                Value = i.Value
            }).OrderBy(i => i.Code));
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetItemModel>> GetItemsAsync(IEnumerable<DataFilter> filters)
        {
            if (filters == null)
                filters = new List<DataFilter>();

            return await _dataContext.Items.ApplyFilter(filters).Select(i => new GetItemModel
            {
                Id = i.Id,
                Code = i.Code,
                Value = i.Value
            }).ToListAsync();
        }
    }
}
