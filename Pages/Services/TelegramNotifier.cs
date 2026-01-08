namespace Tasks_tracker.Pages.Services;


public interface ITelegramNotifier
{
    Task NotifyAsync(string message, CancellationToken ct = default);
}

public class DummyTelegramNotifier : ITelegramNotifier
{
    public Task NotifyAsync(string message, CancellationToken ct = default)
    {
        Console.WriteLine($"[TELEGRAM] {message}");
        
        return Task.CompletedTask;
    }

}