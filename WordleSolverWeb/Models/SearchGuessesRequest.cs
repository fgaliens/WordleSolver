namespace WordleSolverWeb.Models
{
    public class SearchGuessesRequest
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public SearchToken[]? Tokens { get; set; }
    }
}
