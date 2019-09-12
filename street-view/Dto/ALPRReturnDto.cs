using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace street.Dto
{
    public class ALPRReturnDto
    {
        public int version { get; set; }
        public string data_type { get; set; }
        public List<Plate> results { get; set; }

        public class Plate
        {
            public string plate { get; set; }
            public double confidence { get; set; }
            public List<Candidate> candidates { get; set; }
        }

        public class Candidate
        {
            public string plate { get; set; }
            public double confidence { get; set; }
        }
    }
}
