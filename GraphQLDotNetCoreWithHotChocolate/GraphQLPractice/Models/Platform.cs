using System.Collections.Generic;

namespace GraphQLPractice.Models
{
    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string LicensedKey { get; set; }
        public ICollection<Command> Commands { get; set; } = new List<Command>();
    }
}