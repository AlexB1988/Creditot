using Creditot.Domain;
using Creditot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Creditot.Services
{
    public class OperationService:IOperationService
    {
        private readonly DataContext _dataContext;

        public OperationService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task <Credits> GetLast(long chatId)
        {
            return await  _dataContext.Credits.OrderBy(x=>x.CreatedAt).LastOrDefaultAsync(x=>x.ChatId==chatId);
            
        }
    }
}
