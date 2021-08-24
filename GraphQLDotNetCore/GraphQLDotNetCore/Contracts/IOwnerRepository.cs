using GraphQLDotNetCore.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GraphQLDotNetCore.Contracts
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> GetAll();
        Owner GetOwnerById(Guid id);
        Owner CreateNewOwner(Owner owner);
        Owner UpdateOwner(Owner db, Owner owner);
        void DeleteOwner(Owner owner);
    }
}
