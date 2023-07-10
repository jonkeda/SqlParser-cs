using System.Text;

namespace SqlParser.Writers.PostgreSql;

public partial class PostgreSqlTextWriter : SqlTextWriter
{
    public PostgreSqlTextWriter(StringBuilder sb) : base(sb)
    {
    }
}

public static class SqlWritingExtensions
{
    /// <summary>
    /// Writes a IWriteSql object instance and all child objects
    /// to a single PostgresSql string
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static string ToPostgresSql(this IWriteSql sql)
    {
        var builder = StringBuilderPool.Get();

        using (var writer = new PostgreSqlTextWriter(builder))
        {
            sql.ToSql(writer);
        }

        return StringBuilderPool.Return(builder);
    }

}