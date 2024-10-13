namespace saldoFields
{


    public class Room(uint number, decimal square)
    {
        public uint Number { get; private set; } = number;
        public decimal Square { get; private set; } = square;

        public override string ToString()
        {
            return $"Номер комнаты - {Number} | Площадь комнаты - {Square}";
        }
    }
    public class Tariff(int year, int month, decimal amount)
    {
        public string Code => StartDt.ToString("ddMMyyyy");
        public DateTime StartDt { get; private set; } = new DateTime(year, month, 1);
        public decimal Amount { get; private set; } = amount;
        public override string ToString()
        {
            return $"Код тарифа - {Code} | Дата старта тарифа - {StartDt:yyy.MM.dd}";
        }
    }

    public class Oplata_room(decimal oplata_amount)
    {
        public decimal opl_am { get; private set; } = oplata_amount;
    }

    public class Pereraschet_room(decimal perer_amount)
    {
        public decimal perer_am { get; private set; } = perer_amount;
    }



}