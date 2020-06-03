namespace ChordFactory.OpenSilver.repositories
{
    using System.Threading.Tasks;
    using Settings = models.Settings;

    public class FakeSettingsRepository:ISettingsRepository
    {
        public async Task AddSettings(Settings settings)
        {
            await Task.Run(() => throw new System.NotImplementedException());
        }

        public async Task UpdateSettings(Settings settings)
        {
            await Task.Run(() => throw new System.NotImplementedException());
        }

        public async Task<Settings> GetSettings()
        {
            return await Task.Run(() => new Settings());
        }
    }
}