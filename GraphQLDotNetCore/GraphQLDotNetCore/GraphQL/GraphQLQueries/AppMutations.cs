
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
		}
	}
}
