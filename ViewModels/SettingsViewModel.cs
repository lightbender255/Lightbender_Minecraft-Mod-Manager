using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using Lightbender_Minecraft_Mod_Manager.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;

namespace Lightbender_Minecraft_Mod_Manager.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public ICommand BrowseCommand { get; }

        public SettingsViewModel()
        {
            BrowseCommand = new Command<string>(Browse);
            if (App.LoadedSettings != null)
            {
                SourceModsPath = App.LoadedSettings.ModDirectories.SourceModPath;
                ClientModsPath = App.LoadedSettings.ModDirectories.ClientModPath;
                ServerModsPath = App.LoadedSettings.ModDirectories.ServerModPath;
            }
        }

        private string _sourceModsPath;
        
        public string SourceModsPath
        {
            get => _sourceModsPath;
            set
            {
                _sourceModsPath = value;
                OnPropertyChanged();
            }
        }

        private string _clientModsPath;
        public string ClientModsPath
        {
            get => _clientModsPath;
            set
            {
                _clientModsPath = value;
                OnPropertyChanged();
            }
        }

        private string _serverModsPath;
        public string ServerModsPath
        {
            get => _serverModsPath;
            set
            {
                _serverModsPath = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand _saveSettingsCommand;
        public ICommand SaveSettingsCommand
        {
            get
            {
                if (_saveSettingsCommand != null)
                {
                    return _saveSettingsCommand;
                }

                _saveSettingsCommand = new Command(async () =>
                {
                    // Call the Save method of the AppSettings class to save the settings.
                    var appSettings = new Settings();
                    appSettings.ModDirectories.SourceModPath = SourceModsPath;
                    appSettings.ModDirectories.ClientModPath = ClientModsPath;
                    appSettings.ModDirectories.ServerModPath = ServerModsPath;

                    await Settings.SaveAsync(appSettings);
                });
                return _saveSettingsCommand;
            }
        }

        private async void Browse(string fieldName)
        {
            var cancellationToken = new CancellationToken();

            var result = await FolderPicker.Default.PickAsync(cancellationToken);

            if (result.IsSuccessful)
            {
                await Toast.Make($"The folder was picked: Name - {result.Folder.Name}, Path - {result.Folder.Path}", ToastDuration.Long).Show(cancellationToken);

                switch (fieldName)
                {
                    case "SourceModsPath":
                        SourceModsPath = result.Folder.Path;
                        break;
                    case "ClientModsPath":
                        ClientModsPath = result.Folder.Path;
                        break;
                    case "ServerModsPath":
                        ServerModsPath = result.Folder.Path;
                        break;
                }
            }
			else
            {
                await Toast.Make($"The folder was not picked with error: {result.Exception.Message}", ToastDuration.Long).Show(cancellationToken);
            }
		}
	}
}
