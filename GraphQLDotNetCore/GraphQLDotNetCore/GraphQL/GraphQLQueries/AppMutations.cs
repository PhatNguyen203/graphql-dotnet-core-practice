
using GraphQL;
using GraphQL.Types;
using GraphQLDotNetCore.Contracts;
using GraphQLDotNetCore.Entities;
using GraphQLDotNetCore.GraphQL.GraphQLTypes;
using GraphQLDotNetCore.GraphQLSchema.GraphQLTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNetCore.GraphQL.GraphQLQueries
{
	public class AppMutations : ObjectGraphType
	{
		public AppMutations(IOwnerRepository repo)
		{
			Field<OwnerType>(
				"createOwner",
				arguments: new QueryArguments(new QueryArgument<NonNullGraphType<OwnerInputType>> { Name = "owner"}),
				resolve: context => {
					var owner = context.GetArgument<Owner>("owner");
					return repo.CreateNewOwner(owner);
				}
				);
			Field<OwnerType>(
				"updateOwner",
				arguments:	new QueryArguments(
								new QueryArgument<NonNullGraphType<OwnerInputType>> { Name = "owner"},
								new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }),
				resolve: context => {
					var owner = context.GetArgument<Owner>("owner");
					var ownerId = context.GetArgument<Guid>("ownerId");
					var db = repo.GetOwnerById(ownerId);
					if (db == null)
					{
						context.Errors.Add(new ExecutionError("Couldn't find owner in db."));
						return null;
					}
					return repo.UpdateOwner(db, owner);
				}
				);
			Field<StringGraphType>(
				"deleteOwner",
				arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }),
				resolve: context => {
					var ownerId = context.GetArgument<Guid>("ownerId");
					var db = repo.GetOwnerById(ownerId);
					if (db == null)
					{
						context.Errors.Add(new ExecutionError("Couldn't find owner in db."));
						return null;
					}
					repo.DeleteOwner(db);
					return $"The owner with the id: {ownerId} has been successfully deleted from db.";

				}
				);
		}
	}
}
