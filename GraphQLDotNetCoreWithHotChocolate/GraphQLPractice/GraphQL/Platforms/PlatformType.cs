using System.Linq;
using GraphQLPractice.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;

namespace GraphQLPractice.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Description("Represents any software or service that has a command line interface.");
            descriptor
                .Field(p => p.LicensedKey).Ignore();

             descriptor
                .Field(p => p.Id)
                .Description("Represents the unique ID for the platform.");

            descriptor
                .Field(p => p.Name)
                .Description("Represents the name for the platform.");

            descriptor
                .Field(p => p.Commands)
                .ResolveWith<Resolvers>(r => r.GetCommands(default!,default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the list of available commands for this platform.");
        }

        private class Resolvers
        {
            public IQueryable<Command>GetCommands(Platform platform,[ScopedService] AppDbContext context){
                return context.Commands.Where(p => p.PlatformId == platform.Id);
            }
        }
    }
}