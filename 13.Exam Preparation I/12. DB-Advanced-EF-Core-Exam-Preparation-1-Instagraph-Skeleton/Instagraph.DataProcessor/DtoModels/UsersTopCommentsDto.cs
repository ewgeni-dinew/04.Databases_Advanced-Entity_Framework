using System;
using System.Collections.Generic;
using System.Text;

namespace Instagraph.DataProcessor.DtoModels
{
    public class UsersTopCommentsDto
    {
        public string Username { get; set; }
        public int MostComments { get; set; }
    }
}
