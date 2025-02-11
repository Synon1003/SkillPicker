using System.Collections.ObjectModel;
using SkillPicker.Model;

namespace SkillPicker.ViewModel
{
    public class StoredSkillsBrowserViewModel : ViewModelBase
    {
        private StoredSkillsBrowserModel _model;

        public event EventHandler<StoredSkillsEventArgs>? SkillsLoading;
        public event EventHandler<StoredSkillsEventArgs>? SkillsSaving;

        public DelegateCommand NewSaveCommand { get; private set; }

        public ObservableCollection<StoredSkillsViewModel> StoredSkills { get; private set; }

        public StoredSkillsBrowserViewModel(StoredSkillsBrowserModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _model = model;
            _model.StoreChanged += new EventHandler(Model_StoreChanged);

            NewSaveCommand = new DelegateCommand(param =>
            {
                string? fileName = Path.GetFileNameWithoutExtension(param?.ToString()?.Trim());
                if (!String.IsNullOrEmpty(fileName))
                {
                    fileName += ".sav";
                    OnSkillsSaving(fileName);
                }
            });
            StoredSkills = new ObservableCollection<StoredSkillsViewModel>();
            UpdateStoredSkills();
        }

        private void UpdateStoredSkills()
        {
            StoredSkills.Clear();

            foreach (StoredSkillsModel item in _model.StoredSkills)
            {
                StoredSkills.Add(new StoredSkillsViewModel
                {
                    Name = item.Name,
                    Modified = item.Modified,
                    LoadSkillsCommand = new DelegateCommand(param => OnSkillsLoading(param?.ToString() ?? "")),
                    SaveSkillsCommand = new DelegateCommand(param => OnSkillsSaving(param?.ToString() ?? ""))
                });
            }
        }

        private void Model_StoreChanged(object? sender, EventArgs e)
        {
            UpdateStoredSkills();
        }

        private void OnSkillsLoading(String name)
        {
            SkillsLoading?.Invoke(this, new StoredSkillsEventArgs { Name = name });
        }

        private void OnSkillsSaving(String name)
        {
            SkillsSaving?.Invoke(this, new StoredSkillsEventArgs { Name = name });
        }
    }
}
