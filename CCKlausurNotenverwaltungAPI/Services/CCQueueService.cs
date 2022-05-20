using Azure.Storage.Queues;
using CCKlausurNotenverwaltungAPI.Models;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace CCKlausurNotenverwaltungAPI.Services
{
    public class CCQueueService
    {
        public async Task AddMessageAsync(string message)
        {
            QueueClient queueClient = new QueueClient("UseDevelopmentStorage=true", "klausur");
            queueClient.Create();
            await queueClient.SendMessageAsync(Base64Encode(message));
        }

        //public async Task AddLVBewertungAsync(LVBewertung lvBewertung)
        public async Task AddLVBewertungAsync(LVBewertung lvBewertung)
        {
            //TODO - Aufgabe 1

            QueueClient queueClient = new QueueClient("UseDevelopmentStorage=true", "queuelvbewertung");
            queueClient.Create();
            String _message = lvBewertung.LVBewertungId + " " + lvBewertung.StudPersNummer +
            ' ' + lvBewertung.LVBezeichnung + ' ' + lvBewertung.PunkteTheorie + ' ' + lvBewertung.PunktePraxis;
            await queueClient.SendMessageAsync(Base64Encode(_message));

            //throw new NotImplementedException();
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
