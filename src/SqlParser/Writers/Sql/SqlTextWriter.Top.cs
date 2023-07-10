using SqlParser.Ast;

namespace SqlParser
{
    public partial class SqlTextWriter
    {
        internal virtual void ToSql(Top ast)
        {
            var extension = ast.WithTies ? " WITH TIES" : null;

            if (ast.Quantity != null)
            {
                var percent = ast.Percent ? " PERCENT" : null;
                WriteSql($"TOP ({ast.Quantity}){percent}{extension}");
            }
            else
            {
                WriteSql($"TOP{extension}");
            }
        }
    }
}
