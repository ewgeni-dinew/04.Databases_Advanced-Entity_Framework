using System;
using System.Collections.Generic;
using System.Text;

namespace Instagraph.DataProcessor.DtoModels
{
    public class UncommentedPostsDto
    {
        public int Id { get; set; }
        public string Picture { get; set; }
        public string User { get; set; }
    }
}
