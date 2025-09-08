namespace TimeTwoFix.Core.Common
{
    public class GroupCount<TKey>
    {
        public TKey Key { get; set; } = default!;
        public int Count { get; set; }
    }
}