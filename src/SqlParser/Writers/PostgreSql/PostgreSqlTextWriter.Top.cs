using SqlParser.Ast;

namespace SqlParser.Writers.PostgreSql
{
    public partial class PostgreSqlTextWriter
    {
        internal override void ToSql(Top ast)
        {
            if (ast.WithTies)
            {
                throw new NotSupportedException("TOP WITH TIES");
            }
            if (ast.Percent)
            {
                throw new NotSupportedException("TOP PERCENT");
            }
            WriteSql($"LIMIT {ast.Quantity}");
        }
    }
}
