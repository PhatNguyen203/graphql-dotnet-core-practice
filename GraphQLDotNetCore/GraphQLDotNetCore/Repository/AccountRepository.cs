using GraphQLDotNetCore.Contracts;
using GraphQLDotNetCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNetCore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationContext _context;

        public AccountRepository(ApplicationContext context)
        {
            _context = context;
        }

		public async Task<ILookup<Guid, Account>> GetAccountByOwnerIds(IEnumerable<Guid> ownerIds)
		{
			var accounts = await _context.Accounts.Where(x => ownerIds.Contains(x.OwnerId)).ToListAsync();
			return accounts.ToLookup(x => x.OwnerId);
		}

		public IEnumerable<Account> GetAllAccountsPerOwner(Guid ownerId)
		{
            return _context.Accounts.Where(x => ownerId.Equals(x.OwnerId)).ToList();
		}
	}
}
