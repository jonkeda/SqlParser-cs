using SqlParser.Ast;

namespace SqlParser
{
    public partial class SqlTextWriter
    {
        internal virtual void ToSql(Query ast)
        {
            if (ast.With != null)
            {
                WriteSql($"{ast.With} ");
            }

            ast.Body.ToSql(this);

            if (ast.OrderBy != null)
            {
                WriteSql($" ORDER BY {ast.OrderBy}");
            }

            if (ast.Limit != null)
            {
                WriteSql($" LIMIT {ast.Limit}");
            }

            if (ast.Offset != null)
            {
                WriteSql($" {ast.Offset}");
            }

            if (ast.Fetch != null)
            {
                WriteSql($" {ast.Fetch}");
            }

            if (ast.Locks != null && ast.Locks.Any())
            {
                WriteSql($" {ast.Locks.ToSqlDelimited(Symbols.Space.ToString())}");
            }
        }
    }
}
