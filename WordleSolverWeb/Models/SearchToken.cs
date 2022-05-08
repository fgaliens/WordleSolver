namespace WordleSolverWeb.Models
{
    public class SearchToken
    {
        public int Index { get; set; }
        public char Letter { get; set; }
        public SearchTokenState State { get; set; }
    }
}
