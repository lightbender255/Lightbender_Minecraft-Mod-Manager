using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Lightbender_Minecraft_Mod_Manager.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private string _sourceModPath;
        public string SourceModPath
        {
            get => _sourceModPath;
            set
            {
                _sourceModPath = value;
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

        private string _serverModPath;
        public string ServerModPath
        {
            get => _serverModPath;
            set
            {
                _serverModPath = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SaveSettingsCommand
        {
            get
            {
                throw new NotImplementedException("Not Implemented");
                //        if(_saveSettingsCommand == null)
                //        {
                //            _saveSettingsCommand = new Command(async () => 
                //            {
                //                // Save settings to the user's AppData folder

                //            });
                //        }
                //        return _saveSettingsCommand;
            }
        }
    }
}
