namespace QueryAnalyzer.Interfaces
{
    public interface IFilterRuleCriteria
    {
        string FilterStatement { get; }

        string OrderByColumnName { get; }

        string OrderBy { get; }

        string FilterLogic { get; }
    }
}