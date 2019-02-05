using Core.Context;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
	public class AuditoriumService : BaseService<Auditorium, Guid>, IAuditoriumService
	{
		public AuditoriumService(CinemaContext context) : base(context)
		{
		}

		public async Task<List<Auditorium>> GetEagerAllAsync()
		{
			return await context
			   .Auditoriums
			   .AsNoTracking()
			   .Include(auditorium => auditorium.Cinema)
			   .Include(auditorium => auditorium.Events)
			   .ToListAsync();
		}

		public async Task<Auditorium> GetEagerByIdAsync(Guid id)
		{
			return await context
			   .Auditoriums
			   .AsNoTracking()
			   .Include(auditorium => auditorium.Cinema)
			   .Include(auditorium => auditorium.Events)
			   .FirstOrDefaultAsync(auditorium => auditorium.Id.Equals(id));
		}

		public async Task<List<Auditorium>> GetEagerByIdsAsync(List<Guid> ids)
		{
			return await context
				.Auditoriums
				.AsNoTracking()
				.Include(auditorium => auditorium.Cinema)
				.Include(auditorium => auditorium.Events)
				.Join(
					ids,
					auditorium => auditorium.Id,
					id => id,
					(auditorium, id) => auditorium)
				.ToListAsync();
		}

		public IQueryable<Auditorium> GetPagedQuery(string orderBy, bool order)
		{
			var query = GetBaseQuery();

			////string filter = "";
			////query = query.Where(cinema =>
			////    cinema.Name.Contains(filter)
			////    || cinema.Address.Contains(filter)
			////    || cinema.Contact.Contains(filter)
			////    || cinema.Location.Contains(filter));

			switch (orderBy)
			{
				case "Name" when order is true:
					query = query.OrderBy(auditorium => auditorium.Name);
					break;
				case "Name" when order is false:
					query = query.OrderByDescending(auditorium => auditorium.Name);
					break;
				default:
					break;
			}

			return query;
		}

		public async Task<List<Auditorium>> GetPagedAsync(IQueryable<Auditorium> query, int page, int size)
		{
			return await query
				.Skip(page * size)
				.Take(size)
				.ToListAsync();
		}

		public async Task<int> GetPagedCountAsync(IQueryable<Auditorium> pagedQuery)
		{
			return await pagedQuery.CountAsync();
		}
	}
}

