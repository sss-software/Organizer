﻿using Organizer.DataAccess;
using Organizer.Model;
using System.Data.Entity;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace Organizer.UI.Data.Repositories
{
    public class MeetingRepository : GenericRepository<Meeting, OrganizerDbContext>, IMeetingRepository
    {
        public MeetingRepository(OrganizerDbContext context) : base(context)
        {
        }

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            return await Context.Set<Person>().ToListAsync();
        }

        public async override Task<Meeting> GetByIdAsync(int id)
        {
            return await Context.Meetings
                .Include(x => x.Persons)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task ReloadPersonAsync(int personId)
        {
            DbEntityEntry<Person> dbEntityEntry = Context.ChangeTracker.Entries<Person>()
                .SingleOrDefault(x => x.Entity.Id == personId);

            if (dbEntityEntry != null)
            {
                await dbEntityEntry.ReloadAsync();
            }
        }
    }
}
