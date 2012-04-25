using System.Text;

namespace Common.Libraries.Utility.Type
{
    public class TupleType<X, Y, Z>
    {
        public X Left { get; set; }
        public Y Mid { get; set; }
        public Z Right { get; set; }

        public TupleType(X x, Y y, Z z)
        {
            this.Left = x;
            this.Mid = y;
            this.Right = z;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("TupleType<L=");
            sb.Append((this.Left == null) ? "null" : this.Left.ToString());
            sb.Append(", M=");
            sb.Append((this.Mid == null) ? "null" : this.Mid.ToString());
            sb.Append(", R=");
            sb.Append((this.Right == null) ? "null" : this.Right.ToString());
            sb.Append(">");
            return (sb.ToString());

        }
    }
}
