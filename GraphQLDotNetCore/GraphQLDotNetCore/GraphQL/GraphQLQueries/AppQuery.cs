using GraphQL;
using GraphQL.Types;
using GraphQLDotNetCore.Contracts;
using GraphQLDotNetCore.GraphQLSchema.GraphQLTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNetCore.GraphQL.GraphQLQueries
{
	public class AppQuery : ObjectGraphType
	{
		public AppQuery(IOwnerRepository repo)
		{
			Field<ListGraphType<OwnerType>>(
				"owners",
				resolve: context => repo.GetAll()
				);
			Field<OwnerType>(
				"owner",
				arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>{ Name = "ownerId" }),
				resolve: context => 
				{
					Guid id;
					if (!Guid.TryParse(context.GetArgument<string>("ownerId"), out id))
					{
						context.Errors.Add(new ExecutionError("Owner not found!!!!"));
						return null;
					}
					//id = context.GetArgument<Guid>("ownerId"); not need it because of out id
					return repo.GetOwnerById(id);
				}
				);
		}
	}
}
