using System;
using Stations.Data;
using Newtonsoft.Json;
using Stations.DataProcessor.Dto;
using System.Linq;
using System.Text;
using Stations.Models;
using System.Globalization;
using System.Xml.Linq;

namespace Stations.DataProcessor
{
	public static class Deserializer
	{
		private const string FailureMessage = "Invalid data format.";
		private const string SuccessMessage = "Record {0} successfully imported.";

		public static string ImportStations(StationsDbContext context, string jsonString)
		{
            var sb = new StringBuilder();
            var stationsDto = JsonConvert.DeserializeObject<StationsDto[]>(jsonString);

            foreach (var station in stationsDto)
            {
                if (station.Name==null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (station.Town==null)
                {
                    station.Town = station.Name;
                }

                if (context.Stations.Any(s=>s.Name==station.Name))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (station.Name.Length>50||station.Town.Length>50)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                
                var currentStation = new Station()
                {
                    Name = station.Name,
                    Town = station.Town
                };

                context.Stations.Add(currentStation);
                context.SaveChanges();
                sb.AppendLine($"Record {station.Name} successfully imported.");
            }

            return sb.ToString().TrimEnd();
		}

		public static string ImportClasses(StationsDbContext context, string jsonString)
		{
            var sb = new StringBuilder();
            var classesDto = JsonConvert.DeserializeObject<ClassDto[]>(jsonString);

            foreach (var cls in classesDto)
            {
                if (cls.Name == null||cls.Abbreviation==null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (context.SeatingClasses.Any(s=>s.Name==cls.Name||s.Abbreviation==cls.Abbreviation))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (cls.Name.Length>30||cls.Abbreviation.Length!=2)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var currentSeatClass = new SeatingClass()
                {
                    Name = cls.Name,
                    Abbreviation = cls.Abbreviation
                };
                context.SeatingClasses.Add(currentSeatClass);
                context.SaveChanges();
                sb.AppendLine($"Record {cls.Name} successfully imported.");
            }
            return sb.ToString().TrimEnd();
        }

        public static string ImportTrains(StationsDbContext context, string jsonString)
		{
            throw new NotImplementedException();
        }

		public static string ImportTrips(StationsDbContext context, string jsonString)
		{
            throw new NotImplementedException();
        }

		public static string ImportCards(StationsDbContext context, string xmlString)
		{
            var xDoc = XDocument.Parse(xmlString);
            var sb = new StringBuilder();

            foreach (var element in xDoc.Root.Elements())
            {
                var name = element.Element("Name")?.Value;
                int? age = int.Parse(element.Element("Age")?.Value);
                var cardType = element.Element("CardType")?.Value;

                if (age == null||age < 0||age>120||name==null||name.Length>128)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var currentCard = new CustomerCard()
                {
                    Name = name,
                    Age = int.Parse(element.Element("Age").Value),
                };

                switch (cardType)
                {
                    case "Pupil":currentCard.Type = CardType.Pupil;break;
                    case "Student": currentCard.Type = CardType.Student;break;
                    case "Elder": currentCard.Type = CardType.Elder;break;
                    case "Debilitated": currentCard.Type = CardType.Debilitated;break;
                    case null:currentCard.Type = CardType.Normal;break;
                    default :currentCard.Type = CardType.Normal; break;
                }

                context.Cards.Add(currentCard);
                context.SaveChanges();
                sb.AppendLine($"Record {name} successfully imported.");
            }
            return sb.ToString().TrimEnd(); 
		}

		public static string ImportTickets(StationsDbContext context, string xmlString)
		{
            throw new NotImplementedException();
        }
	}
}