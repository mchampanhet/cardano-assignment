namespace Cardano.Models
{
    internal class GleifResponsePagination
    {
        public int currentPage { get; set; }
        public int perPage { get; set; }
        public int total { get; set; }
        public int lastPage { get; set; }
    }
}
