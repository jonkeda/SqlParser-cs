using SqlParser.Ast;

namespace SqlParser.Writers.PostgreSql
{
    public partial class PostgreSqlTextWriter
    {
        internal override void ToSql(Select ast)
        {

            var distinct = ast.Distinct ? " DISTINCT" : null;
            Write($"SELECT{distinct}");

            WriteSql($" {ast.Projection}");

            if (ast.Into != null)
            {
                WriteSql($" {ast.Into}");
            }

            if (ast.From != null)
            {
                WriteSql($" FROM {ast.From}");
            }

            if (ast.LateralViews.SafeAny())
            {
                foreach (var view in ast.LateralViews!)
                {
                    view.ToSql(this);
                }
            }

            if (ast.Selection != null)
            {
                WriteSql($" WHERE {ast.Selection}");
            }

            if (ast.GroupBy != null)
            {
                WriteSql($" GROUP BY {ast.GroupBy}");
            }

            if (ast.ClusterBy != null)
            {
                WriteSql($" CLUSTER BY {ast.ClusterBy}");
            }

            if (ast.DistributeBy != null)
            {
                WriteSql($" DISTRIBUTE BY {ast.DistributeBy}");
            }

            if (ast.SortBy != null)
            {
                WriteSql($" SORT BY {ast.SortBy}");
            }

            if (ast.Having != null)
            {
                WriteSql($" HAVING {ast.Having}");
            }

            if (ast.QualifyBy != null)
            {
                WriteSql($" QUALIFY {ast.QualifyBy}");
            }
        }
    }
}
