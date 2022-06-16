namespace HealthInstitution.Services
{
    public abstract class SearchService : IMatch
    {
        public bool IsMatching(string first, string second)
        {
            return second != "" && first.ToLower().Contains(second.ToLower());
        }

    }
}
