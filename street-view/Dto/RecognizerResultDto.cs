using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace street.Dto
{
    public class RecognizerResultDto
    {
        public Boolean Recognized { get; set; }
        public List<RecognizerGuessDto> Result { get; set; }
    }
}
