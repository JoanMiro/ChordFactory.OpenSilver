namespace ChordFactory.OpenSilver.repositories
{
    using System;
    using System.Threading.Tasks;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Text;
    using System.Windows;
    using Newtonsoft.Json;
    using Settings = models.Settings;

    public class SettingsRepository : ISettingsRepository
    {
        private readonly string filePath;


        public string StatusMessage { get; private set; }

        public SettingsRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public async Task AddSettings(Settings settings)
        {
            if (!BrowserInfo.DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer())
            {
                try
                {
                    if (!settings.IsValid)
                    {
                        throw new ArgumentException("settings");
                    }

                    var jsonData = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(settings));

                    using (var storage = IsolatedStorageFile.GetUserStoreForAssembly())
                    {
                        IsolatedStorageFileStream isolatedStorageFileStream;
                        using (isolatedStorageFileStream = storage.CreateFile(this.filePath))
                        {
                            Encoding encoding = new UTF8Encoding();
                            var bytes = encoding.GetBytes(jsonData);
                            isolatedStorageFileStream.Write(bytes, 0, bytes.Length);
                            isolatedStorageFileStream.Close();
                            // MessageBox.Show("A new file named SampleFile.txt was successfully saved to the storage.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    this.StatusMessage = $"Failed to add settings record. Error: {ex.Message}";
                }
            }
        }

        public async Task UpdateSettings(Settings settings)
        {
            await this.AddSettings(settings);
        }

        public async Task<Settings> GetSettings()
        {
            var jsonData = string.Empty;
            using (var storage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                if (storage.FileExists(this.filePath))
                {
                    using (var fs = storage.OpenFile(this.filePath, System.IO.FileMode.Open))
                    {
                        using (var sr = new StreamReader(fs))
                        {
                            jsonData = await sr.ReadToEndAsync();
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"No file named {this.filePath} was found in the storage.");
                }
            }

            return JsonConvert.DeserializeObject<Settings>(jsonData);
        }
    }
}