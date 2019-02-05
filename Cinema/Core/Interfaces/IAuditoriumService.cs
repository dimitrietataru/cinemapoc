using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAuditoriumService : IBaseService<Auditorium, Guid>
    {
		Task<List<Auditorium>> GetEagerAllAsync();
		Task<Auditorium> GetEagerByIdAsync(Guid id);
		Task<List<Auditorium>> GetEagerByIdsAsync(List<Guid> ids);

		IQueryable<Auditorium> GetPagedQuery(string orderBy, bool order, string filter, bool isExact);
		Task<List<Auditorium>> GetPagedAsync(IQueryable<Auditorium> query, int page, int size);
		Task<int> GetPagedCountAsync(IQueryable<Auditorium> pagedQuery);
	}
}
