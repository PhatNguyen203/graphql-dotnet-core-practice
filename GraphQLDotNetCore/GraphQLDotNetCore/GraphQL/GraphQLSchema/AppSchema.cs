using GraphQL.Types;
using GraphQL.Utilities;
using GraphQLDotNetCore.GraphQL.GraphQLQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNetCore.GraphQLSchema
{
	public class AppSchema : Schema
	{
		public AppSchema(IServiceProvider provider) : base(provider) 
		{
			Query = provider.GetRequiredService<AppQuery>();
			Mutation = provider.GetRequiredService<AppMutations>();
		}
	}
}
