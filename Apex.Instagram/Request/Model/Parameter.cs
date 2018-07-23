namespace Apex.Instagram.Request.Model
{
    internal class Parameter
    {
        private readonly IPostValue _value;

        public Parameter(IPostValue value, bool sign)
        {
            _value = value;
            Sign   = sign;
        }

        public bool Sign { get; }

        public string Serialize() { return _value.Serialize(); }

        public override string ToString() { return _value.ToString(); }
    }
}