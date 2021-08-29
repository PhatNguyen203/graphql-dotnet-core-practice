using System.Threading;
using System.Threading.Tasks;
using GraphQLPractice.GraphQL.Commands;
using GraphQLPractice.GraphQL.Platforms;
using GraphQLPractice.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;

namespace GraphQLPractice.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, [ScopedService] AppDbContext context, [Service]ITopicEventSender sender, CancellationToken cancellationToken )
        {
            var platform = new Platform
            {
                Name = input.Name
            };
            context.Platforms.Add(platform);
            await context.SaveChangesAsync();
            await sender.SendAsync(nameof(Subcription.OnPlatformAdded), platform, cancellationToken);
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