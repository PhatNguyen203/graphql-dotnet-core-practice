using System.Linq;
using GraphQLPractice.Models;
using HotChocolate;
using HotChocolate.Data;

namespace GraphQLPractice.GraphQL
{
    [GraphQLDescription("Represents queries avalaible")]
    public class Query
    {
        [GraphQLDescription("Get queryable all platforms")]
        [UseDbContext(typeof(AppDbContext))]
        public IQueryable<Platform> GetPlatforms([ScopedService] AppDbContext context ){
            return context.Platforms;
        }
        [GraphQLDescription("Gets the queryable command.")]
        [UseDbContext(typeof(AppDbContext))]
        public IQueryable<Command> GetCommands([ScopedService] AppDbContext context){
            return context.Commands;
        }
    }
}