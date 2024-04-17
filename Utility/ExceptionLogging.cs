namespace WebVakt_API.Utility
{
    public interface IExceptionLogging
{
    Task LogErrorAsync(WebVakt_API.Models.Error error);
}

public class ExceptionLogging : IExceptionLogging
{
    private readonly ApplicationDbContext _context;

    public ExceptionLogging(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task LogErrorAsync(WebVakt_API.Models.Error error)
    {
        try
        {
            _context.Errors.Add(error);
            await _context.SaveChangesAsync();
        }
        catch
        {
        }
    }
}

}
