using SqlParser.Ast;

namespace SqlParser.Writers.PostgreSql
{
    public partial class PostgreSqlTextWriter
    {
        internal override void ToSql(Expression.Function ast)
        {
            if (ast.Special)
            {
                ast.Name.ToSql(this);
            }
            else
            {
                (bool yes, string name, bool brackets) = GetFunctionName(ast);
                if (yes)
                {
                    if (brackets)
                    {
                        WriteSql($"{name}({ast.Args})");
                    }
                    else
                    {
                        WriteSql($"{name}");
                    }
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

        private (bool, string?, bool) GetFunctionName(Expression.Function ast)
        {
            if (ast.Name.Values.Count != 1)
            {
                return (false, null, false);
            }

            Ident ident = ast.Name.Values.First();
            if (ident.QuoteStyle != null)
            {
                return (false, null, false);
            }

            if (Renames.TryGetValue(ident.Value.ToUpper(), out var renamed))
            {
                return (true, renamed.Rename, renamed.Brackets);
            }

            return (false, null, false);
        }

        public static RenamedDictionary Renames { get; } = CreateRenames();

        public static RenamedDictionary CreateRenames()
        {
            var renames = new RenamedDictionary
            {
                { "LEN", "LENGTH", true },
                { "GETDATE", "CURRENT_DATE", false }
            };

            return renames;
        }

        public record Renamed(string Rename, bool Brackets)
        { }

        public class RenamedDictionary : Dictionary<string, Renamed>
        {
            public void Add(string name, string rename, bool brackets)
            {
                this.Add(name.ToUpper(), new Renamed(rename, brackets));
            }
        }
    }
}
