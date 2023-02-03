namespace CodeEvents.Api.Data.Repositories
{
    public class UnitOfWork
    {
        private readonly CodeEventsApiContext db;

        public CodeEventRepository codeEventRepository { get; }

        public UnitOfWork(CodeEventsApiContext db)
        {
            this.db = db;
            codeEventRepository = new CodeEventRepository(db);
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
