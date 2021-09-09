using System;
using Xunit;

namespace ppedv.GMEStore.Data.EFCore.Tests
{
    public class EfContextTests
    {
        [Fact]
        public void Can_create_new_db()
        {
            var con = new EfContext();

            con.Database.EnsureDeleted();
            Assert.False(con.Database.CanConnect());

            con.Database.EnsureCreated();
            Assert.True(con.Database.CanConnect());
        }
    }
}
