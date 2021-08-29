using System.Threading.Tasks;
using GraphQLPractice.GraphQL.Commands;
using GraphQLPractice.GraphQL.Platforms;
using GraphQLPractice.Models;
using HotChocolate;
using HotChocolate.Data;

namespace GraphQLPractice.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, [ScopedService] AppDbContext context)
        {
            var platform = new Platform
            {
                Name = input.Name
            };
            context.Platforms.Add(platform);
            await context.SaveChangesAsync();
            return new AddPlatformPayload(platform);
        } 
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context)
        {
            var command = new Command
            {
                CommandLine = input.CommandLine,
                HowTo = input.HowTo,
                PlatformId = input.PlatformId
            };
            context.Commands.Add(command);
            await context.SaveChangesAsync();
            return new AddCommandPayload(command);
        }
    }
}