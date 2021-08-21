using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQLDotNetCore.Contracts;
using GraphQLDotNetCore.Entities;
using GraphQLDotNetCore.GraphQL.GraphQLTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNetCore.GraphQLSchema.GraphQLTypes
{
	public class OwnerType : ObjectGraphType<Owner>
	{
		public OwnerType(IAccountRepository repo, IDataLoaderContextAccessor dataLoader)
		{
			Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the owner object.");
			Field(x => x.Name).Description("Name property from the owner object");
			Field(x => x.Address).Description("Address property from the owner object");
			Field<ListGraphType<AccountType>>(
				"accounts",
				resolve: context => 
				{
					var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<Guid, Account>("GetAccountByOwnerIds", repo.GetAccountByOwnerIds);
					return loader.LoadAsync(context.Source.Id);
				}
			); ;
		}
	}
}
