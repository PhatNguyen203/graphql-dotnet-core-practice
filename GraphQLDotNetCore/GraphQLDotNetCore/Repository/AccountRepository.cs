using GraphQLDotNetCore.Contracts;
using GraphQLDotNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLDotNetCore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationContext _context;

        public AccountRepository(ApplicationContext context)
        {
            _context = context;
        }

		public IEnumerable<Account> GetAllAccountsPerOwner(Guid ownerId)
		{
            return _context.Accounts.Where(x => ownerId.Equals(x.OwnerId)).ToList();
		}
	}
}
