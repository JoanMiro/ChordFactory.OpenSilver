namespace ChordFactory.OpenSilver.repositories
{
    using System.Threading.Tasks;
    using models;

    public interface ISettingsRepository
    {
        Task AddSettings(Settings settings);

        Task UpdateSettings(Settings settings);

        Task<Settings> GetSettings();
    }
}