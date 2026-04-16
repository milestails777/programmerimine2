using System.Collections.ObjectModel;

namespace KooliProjekt.WpfApplication
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        private readonly IApiClient _apiClient;
        private readonly ObservableCollection<Project> _data;

        private Project _selectedItem;

        public MainWindowViewModel() : this(new ApiClient())
        {

        }

        public MainWindowViewModel(IApiClient apiClient)
        {
            _data = new ObservableCollection<Project>();
            _apiClient = apiClient;
        }

        public async Task LoadDataAsync()
        {
            var data = await _apiClient.List(1, 100);

            _data.Clear();
            foreach (var item in data.Value.Results)
            {
                _data.Add(item);
            }
        }

        public ObservableCollection<Project> Data
        {
            get
            {
                return _data;
            }
        }

        public Project SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();
            }
        }
    }
}
