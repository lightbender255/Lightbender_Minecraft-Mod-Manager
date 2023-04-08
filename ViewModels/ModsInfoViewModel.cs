using CommunityToolkit.Mvvm.Input;
using Lightbender_Minecraft_Mod_Manager.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Lightbender_Minecraft_Mod_Manager.ViewModels;
internal class ModsInfoViewModel : BindableObject
{
    private readonly ModInfo _modInfo;
    //public ObservableCollection<ViewModels.ModInfoViewModel> AllModsInfo { get; }
    public ICommand SelectModCommand { get; }
    private readonly Settings appSettings;

    public ModsInfoViewModel(ModInfo modInfo)
    {
        appSettings = Settings.LoadAsync().Result;
        AllModsInfo = new ObservableCollection<ModInfoViewModel>(Models.ModInfo.LoadAll(appSettings.ModDirectories.SourceModPath).Select(m => new ModInfoViewModel(m)));
 
        SelectModCommand = new AsyncRelayCommand<ViewModels.ModInfoViewModel>(SelectModAsync);
    }

    public ObservableCollection<ViewModels.ModInfoViewModel> AllModsInfo { get; }

    private async Task SelectModAsync(ViewModels.ModInfoViewModel note)
    {
        if (note != null)
            await Shell.Current.GoToAsync($"{nameof(Views.ModManagerPage)}?load={note.Name}");
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