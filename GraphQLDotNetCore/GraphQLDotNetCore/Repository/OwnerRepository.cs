using GraphQLDotNetCore.Contracts;
using GraphQLDotNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLDotNetCore.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationContext _context;

        public OwnerRepository(ApplicationContext context)
        {
            _context = context;
        }

		public IEnumerable<Owner> GetAll()
		{
            return _context.Owners.ToList();
		}

		public Owner GetOwnerById(Guid id)
		{
			return _context.Owners.SingleOrDefault(x => x.Id.Equals(id));
		}
	}
}
