using SqlParser.Ast;

namespace SqlParser.Writers.PostgreSql
{
    public partial class PostgreSqlTextWriter
    {
        internal override void ToSql(Ident ident)
        {
            switch (ident.QuoteStyle)
            {
                case Symbols.DoubleQuote:
                case Symbols.SingleQuote:
                case Symbols.Backtick:

                    WriteSql($"{ident.QuoteStyle}{ident.Value.EscapeQuotedString(ident.QuoteStyle.Value)}{ident.QuoteStyle}");
                    break;

                case Symbols.SquareBracketOpen:
                    Write($@"""{ident.Value}""");
                    break;

                default:
                    Write(ident.Value);
                    break;
            }

        }
    }
}
