namespace MarketIndexAnalyzer.Models.Index;

public class IndexDataModel
{
    public int Id { get; set; }
    public double Open { get; set; }
    public double Close { get; set; }
    public double  High { get; set; }
    public double Low { get; set; }
    public DateTime Time { get; set; }
    public int IndexTypeId { get; set; }
}