using SqlParser.Ast;

namespace SqlParser
{
    public partial class SqlTextWriter
    {
        internal virtual void ToSql(Expression.Function ast)
        {
            if (ast.Special)
            {
                ast.Name.ToSql(this);
            }
            else
            {
                var distinct = ast.Distinct ? "DISTINCT " : null;
                WriteSql($"{ast.Name}({distinct}{ast.Args})");

                if (ast.Over != null)
                {
                    WriteSql($" OVER ({ast.Over})");
                }
            }

        }
    }
}
