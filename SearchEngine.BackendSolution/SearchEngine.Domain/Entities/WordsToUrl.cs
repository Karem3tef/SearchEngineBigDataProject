using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Domain.Entities
{
    public class WordsToUrl
    {
        [Key]
        public string Word { get; set; }
        public string Urls { get; set; }
    }
}
