using System;
using System.Collections.Generic;
using System.Text;

namespace Stations.Models
{
    public class Train
    {
        public Train()
        {
            this.TrainSeats = new List<TrainSeat>();
            this.Trips = new List<Trip>();
        }
        public int Id { get; set; }
        public string TrainNumber { get; set; }
        public TrainType? Type { get; set; }

        public ICollection<Trip> Trips { get; set; }
        public ICollection<TrainSeat> TrainSeats { get; set; }
    }
}
