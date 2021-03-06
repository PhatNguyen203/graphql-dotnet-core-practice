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

		public Owner CreateNewOwner(Owner owner)
		{
			owner.Id = new Guid();
			_context.Add(owner);
			_context.SaveChanges();
			return owner;
		}

		public void DeleteOwner(Owner owner)
		{
			_context.Remove(owner);
			_context.SaveChanges();
		}

		public IEnumerable<Owner> GetAll()
		{
            return _context.Owners.ToList();
		}

		public Owner GetOwnerById(Guid id)
		{
			return _context.Owners.SingleOrDefault(x => x.Id.Equals(id));
		}

		public Owner UpdateOwner(Owner db, Owner owner)
		{
			db.Name = owner.Name;
			db.Address = owner.Address;
			_context.SaveChanges();
			return db;
		}
	}
}
