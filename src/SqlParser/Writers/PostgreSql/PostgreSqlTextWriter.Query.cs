using SqlParser.Ast;
using System.Collections.Generic;
using static SqlParser.Ast.SetExpression;

namespace SqlParser.Writers.PostgreSql
{
    public partial class PostgreSqlTextWriter
    {
        internal override void ToSql(Query ast)
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

            if (ast.Body is SelectExpression selectExpression)
            {
                if (selectExpression.Select.Top != null)
                {
                    WriteSql($" {selectExpression.Select.Top}");
                }
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
