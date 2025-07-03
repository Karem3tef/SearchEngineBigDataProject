using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Domain.Entities
{
    public class UrlPageRank
    {
        [Key]
        public string Url { get; set; }
        public double Rank { get; set; }
    }
}
