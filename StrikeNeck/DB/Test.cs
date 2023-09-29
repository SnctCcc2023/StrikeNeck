using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace strikeneck.DB
{
    public class Test
    {
        [Key]
        public DateTime Date { get; set; }
        public byte ActivationTime { get; set; }
        public byte FLDTime { get; set; }
    }
}
