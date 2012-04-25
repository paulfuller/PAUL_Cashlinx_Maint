using System.Text;

namespace Common.Libraries.Utility.Type
{
    public class PairType<U, V>
    {
        public U Left { get; set; }
        public V Right { get; set; }

        public PairType(U leftVal, V rightVal)
        {
            this.Left = leftVal;
            this.Right = rightVal;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PairType<L=");
            sb.Append(((this.Left == null) ? "null" : this.Left.ToString()));
            sb.Append(", R=");
            sb.Append(((this.Right == null) ? "null" : this.Right.ToString()));
            sb.Append(">");
            return (sb.ToString());
        }
    }
}
