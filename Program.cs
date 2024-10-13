using saldoInMemory;
using saldoFields;

List<Room> rooms0 = [
                new(1,10),
                new(2,20),
                new(3,30)
                ];


        List<Tariff> tariffs0 = [
            new(2024, 5, 5m),
            new(2024, 7, 8m)
            ];

        sR_InMemory.PrintFinalReport(rooms0, tariffs0);
    
   

   