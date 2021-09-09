using System;
using System.Collections.Generic;

namespace ppedv.GMEStore.Model
{
    public class Game : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Published { get; set; }

        public Company Publisher { get; set; }
        public Company Developer { get; set; }
        public ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();

    }
}
