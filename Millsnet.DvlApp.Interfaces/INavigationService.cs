namespace Millsnet.DvlApp.Interfaces
{
    public interface INavigationService
    {
        Task NavigateAsync<T>(object data = null) where T : class;
        Task BackAsync(object data = null);

        void Initialise();

        event EventHandler<EventArgs> Initialised;
    }
}