using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TechTest.Model;

namespace TechTest.Helper
{
    public class RecordMap : ClassMap<Record>
    {
        public RecordMap()
        {
            Map(m => m.caller_id).Name("caller_id");
            Map(m => m.recipient).Name("recipient");
            Map(m => m.call_date).Name("call_date").TypeConverterOption.Format("dd/MM/yyyy"); 
            Map(m => m.end_time).Name("end_time").TypeConverterOption.Format("HH:mm:ss");
            Map(m => m.duration).Name("duration");
            Map(m => m.cost).Name("cost");
            Map(m => m.reference).Name("reference");
            Map(m => m.currency).Name("currency");
        }
    }
}
