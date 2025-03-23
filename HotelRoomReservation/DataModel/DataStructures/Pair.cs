namespace DataModel.DataStructures
{
    public class Pair<TF, TS>
    {
        public Pair() { }

        public Pair(TF first, TS second)
        {
            First = first;
            Second = second;
        }

        public TF First { get; set; }
        public TS Second { get; set; }
    }
}
