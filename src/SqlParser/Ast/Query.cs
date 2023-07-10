namespace SqlParser.Ast;

/// <summary>
/// The most complete variant of a SELECT select expression, optionally
/// including WITH, UNION / other set operations, and ORDER BY.
/// </summary>
public record Query([Visit(1)] SetExpression Body) : IWriteSql, IElement
{
    [Visit(0)] public With? With { get; init; }
    [Visit(2)] public Sequence<OrderByExpression>? OrderBy { get; set; }
    [Visit(3)] public Expression? Limit { get; init; }
    [Visit(4)] public Offset? Offset { get; init; }
    [Visit(5)] public Fetch? Fetch { get; init; }
    [Visit(6)] public Sequence<LockClause>? Locks { get; init; }

    public static implicit operator Query(Statement.Select select)
    {
        return select.Query;
    }

    public static implicit operator Statement.Select(Query query)
    {
        return new Statement.Select(query);
    }

    public void ToSql(SqlTextWriter writer)
    {
       writer.ToSql(this);
    }
}