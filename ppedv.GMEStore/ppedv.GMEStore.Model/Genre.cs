using System.Collections.Generic;

namespace ppedv.GMEStore.Model
{
    public class Genre : Entity
    {
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; } = new HashSet<Game>();

    }
}
