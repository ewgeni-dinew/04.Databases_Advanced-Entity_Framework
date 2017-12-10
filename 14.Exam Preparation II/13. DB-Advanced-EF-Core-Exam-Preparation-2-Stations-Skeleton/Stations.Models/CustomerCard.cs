using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stations.Models
{
    public class CustomerCard
    {
        public CustomerCard()
        {
            this.BoughtTickets = new List<Ticket>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        [Range(0,120)]
        public int Age { get; set; }
        public CardType Type { get; set; }

        public ICollection<Ticket> BoughtTickets { get; set; }
    }
}
