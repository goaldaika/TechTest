using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace TechTest.Model
{
    public class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? caller_id { get; set; }
        public string recipient { get; set; }
        public DateOnly call_date { get; set; }
        public TimeOnly end_time { get; set; }
        public int duration { get; set; }
        public double cost { get; set; }
        public string reference { get; set; }
        public string currency { get; set; }
    }
}
