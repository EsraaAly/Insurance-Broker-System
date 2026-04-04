namespace InsuranceBrokerSystem.Infrastructure.Repositories.Financial
{
    public class RepoAccountNumber: GenericRepository<Account>, IRepoAccountNumber
    {
        private readonly AppDbContext _context;
        private const int MaxLevel = 4;
        private const int MaxSegmentLength = 3;

        public RepoAccountNumber(AppDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<string> GenerateAsync(Account? parent, IEnumerable<Account>? siblings,int AccountType)
        {
            // 1. Determine Level (Root is Level 1)
            int currentLevel = parent == null ? 1 : parent.Level + 1;

            if (currentLevel > MaxLevel)
                throw new Exception($"Max level of {MaxLevel} reached.");

            // 2. Find the next index for this level
            // If siblings is null or empty, start at 1
            int nextIndex = (siblings != null && siblings.Any())
                ? siblings.Select(a => GetSegment(a.AccountNumber, currentLevel)).Max() + 1
                : 1;

            // 3. Initialize the segments array (e.g., [0,0,0,0])
            var segments = new int[MaxLevel];

            // 4. If there is a parent, copy its segments to keep the hierarchy
            if (parent != null)
            {
                var parentSegments = ParseSegments(parent.AccountNumber);
                // Copy only up to the parent's level
                for (int i = 0; i < parent.Level; i++)
                {
                    segments[i] = parentSegments[i];
                }
            }

            // 5. Set the new index at the current level
            segments[currentLevel - 1] = nextIndex;

            if(parent == null)
                segments[0] = AccountType;
            // 6. Format as 001-000-000-000
            return string.Join("-", segments.Select(s => s.ToString().PadLeft(MaxSegmentLength, '0')));
        }

        private int[] ParseSegments(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber)) return new int[MaxLevel];

            return accountNumber
                .Split('-')
                .Select(s => int.TryParse(s, out int val) ? val : 0)
                .ToArray();
        }

        private int GetSegment(string accountNumber, int level)
        {
            var parts = accountNumber.Split('-');
            if (parts.Length < level) return 0;
            return int.TryParse(parts[level - 1], out int val) ? val : 0;
        }
    }
}
