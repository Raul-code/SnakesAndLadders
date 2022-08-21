namespace SnakesAndLadders.Services
{
    public interface INextTurnService
    {
        Task<bool> NextTurnAsync();
    }
}
