using CommunityToolkit.Mvvm.ComponentModel;
using Lightbender_Minecraft_Mod_Manager.Models;
using System.Collections.ObjectModel;

namespace Lightbender_Minecraft_Mod_Manager.ViewModels
{
    internal class ModInfoViewModel : ObservableObject
    {
        private readonly Models.ModInfo _modInfo;

        public ModInfoViewModel(ModInfo modInfo)
        {
            _modInfo = modInfo;

            EntryPoints = new ObservableCollection<string>();
            foreach (var entryPoint in modInfo.EntryPoints)
            {
                EntryPoints.Add(entryPoint.Key);
            }
        }

        public string Name
        {
            get { return _modInfo.Name; }
            set
            {
                _modInfo.Name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _modInfo.Description; }
            set
            {
                _modInfo.Description = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> EntryPoints { get; set; }
    }
}
