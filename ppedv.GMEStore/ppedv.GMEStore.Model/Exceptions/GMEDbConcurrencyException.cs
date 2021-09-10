using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.GMEStore.Model.Exceptions
{
    public class GMEDbConcurrencyException : Exception
    {
        public GMEDbConcurrencyException(Exception inner, Entity dbVersion, Entity myVersion)
        {
            Inner = inner;
            DbVersion = dbVersion;
            MyVersion = myVersion;
        }

        public Exception Inner { get; }
        public Entity DbVersion { get; }
        public Entity MyVersion { get; }

    }

}
