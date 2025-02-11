using SkillPicker.Persistence;
using SkillPicker.Model;
using SkillPicker.ViewModel;
using SkillPicker.View;

namespace SkillPicker
{
    public partial class AppShell : Shell
    {
        private ISkillPickerModel _skillPickerModel;
        private SkillPickerViewModel _skillPickerViewModel;

        private Store _store;
        private StoredSkillsBrowserModel _storeModel;
        private StoredSkillsBrowserViewModel _storeViewModel;

        private LoadSkillsPage _loadSkillsPage;
        private SaveSkillsPage _saveSkillsPage;

        public AppShell(ISkillPickerModel skillPickerModel)
        {
            InitializeComponent();

            _skillPickerModel = skillPickerModel;

            _skillPickerViewModel = new SkillPickerViewModel(_skillPickerModel);
            _skillPickerViewModel.LoadSkills += new EventHandler(SkillPickerViewModel_LoadSkills);
            _skillPickerViewModel.SaveSkills += new EventHandler(SkillPickerViewModel_SaveSkills);

            _store = new Store();
            _storeModel = new StoredSkillsBrowserModel(_store);
            _storeViewModel = new StoredSkillsBrowserViewModel(_storeModel);
            _storeViewModel.SkillsLoading += new EventHandler<StoredSkillsEventArgs>(StoreViewModel_SkillsLoading);
            _storeViewModel.SkillsSaving += new EventHandler<StoredSkillsEventArgs>(StoreViewModel_SkillsSaving);

            _loadSkillsPage = new LoadSkillsPage();
            _loadSkillsPage.BindingContext = _storeViewModel;

            _saveSkillsPage = new SaveSkillsPage();
            _saveSkillsPage.BindingContext = _storeViewModel;

            BindingContext = _skillPickerViewModel;
        }

        private async void SkillPickerViewModel_LoadSkills(object? sender, System.EventArgs e)
        {
            await _storeModel.UpdateAsync();
            await Navigation.PushAsync(_loadSkillsPage);
        }

        private async void SkillPickerViewModel_SaveSkills(object? sender, System.EventArgs e)
        {
            await _storeModel.UpdateAsync();
            await Navigation.PushAsync(_saveSkillsPage);
        }

        private async void StoreViewModel_SkillsLoading(object? sender, StoredSkillsEventArgs e)
        {
            await Navigation.PopAsync();

            try
            {
                await _skillPickerModel.LoadSkillsAsync(Path.Combine(FileSystem.AppDataDirectory, e.Name));
            }
            catch
            {
                await DisplayAlert("Error!", "Loading was unsuccessful.", "OK");
            }
        }

        private async void StoreViewModel_SkillsSaving(object? sender, StoredSkillsEventArgs e)
        {
            await Navigation.PopAsync();

            try
            {
                await _skillPickerModel.SaveSkillsAsync(Path.Combine(FileSystem.AppDataDirectory, e.Name));
                await _storeModel.UpdateAsync();
            }
            catch
            {
                await DisplayAlert("Error!", "Saving was unsuccessful.", "OK");
            }
        }
    }
}
