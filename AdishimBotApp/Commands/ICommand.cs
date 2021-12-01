namespace AdishimBotApp.Commands
{
    public interface ICommand
    {
        Task<bool> TryExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}