using Microsoft.EntityFrameworkCore;

namespace saldoInMemory
{
    public class saldoItems : DbContext
    {
        public saldoItems(DbContextOptions<saldoItems> options) : base(options) { }  //konstruktor
        public DbSet<saldoRow> saldoRows { get; set; }
    }

    public class saldoRow
    {
        public int saldoRowId { get; set; }
        public string perio { get; set; }
        public decimal saldoIncome { get; set; }
        public decimal nachisleno { get; set; }
        public decimal perer { get; set; }
        public decimal itogoNach { get; set; }
        public decimal oplach { get; set; }
        public decimal saldoOutcome { get; set; }

    }
}