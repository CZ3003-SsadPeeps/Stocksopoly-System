internal abstract class NameValidationError
{
    internal int Pos { get; }

    protected NameValidationError(int pos)
    {
        Pos = pos;
    }

    internal class IsBlank : NameValidationError
    {
        internal IsBlank(int pos) : base(pos) { }
    }

    internal class Clash : NameValidationError
    {
        internal int WithPos { get; }

        internal Clash(int pos, int withPos) : base(pos)
        {

            WithPos = withPos;
        }
    }
}
