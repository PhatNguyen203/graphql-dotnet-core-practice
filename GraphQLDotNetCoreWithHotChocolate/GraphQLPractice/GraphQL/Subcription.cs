using GraphQLPractice.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQLPractice.GraphQL
{
    public class Subcription
    {
        [Subscribe]
        [Topic]
        public Platform OnPlatformAdded([EventMessage] Platform platform)
        {
            return platform;
        } 
    }
}