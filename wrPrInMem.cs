using Microsoft.EntityFrameworkCore;
using saldoFields;
using saldoInMemory;

public class sR_InMemory
{

    public static void PrintFinalReport(List<Room> rooms, List<Tariff> tariffs)
    {
        foreach (var r in rooms)
        {
            sR_InMemory.PrintReport(r, tariffs, new DateTime(2024, 4, 1), new DateTime(2024, 8, 1));
            Console.WriteLine();
            Console.WriteLine();
        }
    }



    public static void PrintReport(Room r, List<Tariff> tariffs, DateTime periodFrom, DateTime periodTo)
    {

        sR_InMemory.printHead(r);


        decimal saldo_init = 0, itogo_nachisl = 0, saldo_out = 0;

        var optio1 = new DbContextOptionsBuilder<saldoItems>().UseInMemoryDatabase(databaseName: "saldo_vedomos").Options;

        using (var contextt1 = new saldoItems(optio1))
        {
            int delta = periodTo.Month - periodFrom.Month + 1;
            do
            {
                var actualTariff = tariffs.OrderByDescending(t => t.StartDt).FirstOrDefault(t => t.StartDt <= periodFrom);

                if (actualTariff == null)
                {
                    const int sh = 15;
                    Console.WriteLine($"|{periodFrom,-sh:MMMMyyyy}|{"-",sh}|{"-",sh}|{"-",sh}|{"-",sh}|{"-",sh}|{"-",sh}|");
                }
                else
                {
                    Pereraschet_room recount = new(-1);//recount.perer_am
                    Oplata_room payment = new(5);//payment.opl_am 

                    sR_InMemory.printTableRow(periodFrom, saldo_init, actualTariff, r, recount, itogo_nachisl, payment, out saldo_out);

                    //write row in InMemoryDB      static saldoRow wr_saldoRow_inMemoryDB(arguments of the table)
                    var saldorow0 = sR_InMemory.wr_saldoRow_inMemoryDB(periodFrom, saldo_init, actualTariff, r,
                    recount, itogo_nachisl, payment, saldo_out);

                    contextt1.saldoRows.Add(saldorow0);
                    contextt1.SaveChanges();
                    //----------------------------------------------------------------

                    saldo_init = saldo_out;

                }
                periodFrom = periodFrom.AddMonths(1); //increment cikla - v obshem sluchae nuzhen schetchik zapisei

            }

            while (periodFrom <= periodTo);

            sR_InMemory.printInMemoryDB(contextt1, delta, r);

        }
        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------");

    }

    public static saldoRow wr_saldoRow_inMemoryDB(DateTime periodFrom, decimal saldo_init, Tariff actualTariff, Room r,
                                           Pereraschet_room recount, decimal itogo_nachisl, Oplata_room payment, decimal saldo_out)
    {
        var saldrow0 = new saldoRow
        {
            perio = periodFrom.Month.ToString(),
            saldoIncome = saldo_init,
            nachisleno = actualTariff.Amount * r.Square,
            perer = recount.perer_am,
            itogoNach = itogo_nachisl,
            oplach = payment.opl_am,
            saldoOutcome = saldo_out
        };
        return saldrow0;
    }
    public static void printInMemoryDB(saldoItems contexttt, int deltaa, Room r)
    {
        var contextt1 = contexttt;
        int delta = deltaa;

        foreach (var item in contextt1.saldoRows)
        {

            if (item.saldoRowId > (r.Number - 1) * (delta - 1))
            {

                Console.Write((r.Number - 1) * (delta - 1) + " ");

                Console.Write(item.saldoRowId + " ");
                Console.Write(item.perio + " ");
                Console.Write(item.saldoIncome + " ");
                Console.Write(item.nachisleno + " ");
                Console.Write(item.perer + " ");
                Console.Write(item.itogoNach + " ");
                Console.Write(item.oplach + " ");
                Console.WriteLine(item.saldoOutcome);
            }
        }
    }


    public static void printTableRow(DateTime periodFrom, decimal saldo_init, Tariff actualTariff, Room r,
                Pereraschet_room recount, decimal itogo_nachisl, Oplata_room payment, out decimal saldo_out)
    {
        const int shh = 15;
        itogo_nachisl = actualTariff.Amount * r.Square + recount.perer_am;
        saldo_out = saldo_init + itogo_nachisl - payment.opl_am;
        Console.WriteLine($"|{periodFrom,-shh:MMMMyyyy}|{saldo_init,shh}|{(actualTariff.Amount * r.Square),shh}|{recount.perer_am,shh}|{itogo_nachisl,shh}|{payment.opl_am,shh}|{saldo_out,shh}|");

    }

    public static void printHead(Room r)
    {
        Console.WriteLine($"Комната №{r.Number}");
        const int sh0 = 15;
        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine($"|{" Период ",-sh0}|{" Сальдо входящее ",-sh0}|{" Начислено ",-sh0}|{" Перерасчет ",-sh0}|{" Итого начислено ",-sh0}|{" Оплачено ",-sh0}|{" Сальдо исходящее ",-sh0}|");
        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------");


    }
}