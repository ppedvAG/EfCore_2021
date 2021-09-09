using System.Collections.Generic;

namespace ppedv.GMEStore.Model
{
    public class Company : Entity
    {
        public string Name { get; set; }

        public ICollection<Game> Published { get; set; } = new HashSet<Game>();
        public ICollection<Game> Developed { get; set; } = new HashSet<Game>();
    }
}
