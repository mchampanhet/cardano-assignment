namespace Cardano.Models
{
    internal class GleifResponse
    {
        public GleifResponseMeta meta { get; set; }
        public List<GleifResponseLeiRecord> data { get; set; }
    }
}
