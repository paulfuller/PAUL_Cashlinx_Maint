namespace Common.Libraries.Utility.Type
{
    public class QuadType<A, B, C, D>
    {
        public A X
        {
            get; set;
        }
        public B Y
        {
            get; set;
        }
        public C Z
        {
            get; set;
        }
        public D W
        {
            get; set;
        }

        public QuadType()
        {
            X = default(A);
            Y = default(B);
            Z = default(C);
            W = default(D);
        }
        
        public QuadType(A x, B y, C z, D w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}
