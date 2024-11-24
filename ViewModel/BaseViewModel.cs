using CommunityToolkit.Mvvm.ComponentModel;

namespace MoviesExample.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        string title;
    }
}
