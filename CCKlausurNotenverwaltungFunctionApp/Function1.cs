using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CCKlausurNotenverwaltungFunctionApp
{
    public class LVGesamtbewertung
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string PersonenKennzeichen { get; set; }
        public double PunkteGesamt { get; set; }
    }

    public class LVBewertung
    {
        public int LVBewertungId { get; set; }

        public int StudPersNummer { get; set; }

        public string LVBezeichnung { get; set; }
        public double PunkteTheorie { get; set; }
        public double PunktePraxis { get; set; }
    }
    public class Function1
    {
        [FunctionName("Function1")]
        [return: Table("Bewertungen", Connection = "mylocaltable")]
        public LVGesamtbewertung Run([QueueTrigger("queuelvbewertung", Connection = "mylocalqueue")]string myQueueItem, ILogger log)
        {

            char[] spearator = { ' ' };
            string[] _message = myQueueItem.Split(spearator);

            LVBewertung lVBewertung = new LVBewertung();
            lVBewertung.LVBewertungId = Convert.ToInt32(_message[0]);
            lVBewertung.StudPersNummer = Convert.ToInt32(_message[1]);
            lVBewertung.LVBezeichnung = _message[2];
            lVBewertung.PunkteTheorie = Convert.ToDouble(_message[3]);
            lVBewertung.PunktePraxis = Convert.ToDouble(_message[4]);

            //log.LogInformation($"C# Queue trigger function processed: {lVBewertung}");
            //log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            return new LVGesamtbewertung
            {
                PartitionKey = lVBewertung.LVBezeichnung,
                RowKey = Guid.NewGuid().ToString(),
                PersonenKennzeichen = _message[1],
                PunkteGesamt = lVBewertung.PunkteTheorie + lVBewertung.PunktePraxis
            };


        }
    }
}
