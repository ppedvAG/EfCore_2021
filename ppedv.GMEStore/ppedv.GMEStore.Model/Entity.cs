using System;

namespace ppedv.GMEStore.Model
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
    }
}
