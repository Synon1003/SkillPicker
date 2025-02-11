using SkillPicker.Persistence;

namespace SkillPicker.Model
{
    public class StoredSkillsBrowserModel
    {
        private Store _store;

        public event EventHandler? StoreChanged;

        public StoredSkillsBrowserModel(Store store)
        {
            _store = store;

            StoredSkills = new List<StoredSkillsModel>();
        }

        public List<StoredSkillsModel> StoredSkills { get; private set; }

        public async Task UpdateAsync()
        {
            if (_store == null)
                return;

            StoredSkills.Clear();

            foreach (String name in await _store.GetFilesAsync())
            {
                if (name == "SuspendedSkills")
                    continue;

                StoredSkills.Add(new StoredSkillsModel
                {
                    Name = name,
                    Modified = await _store.GetModifiedTimeAsync(name)
                });
            }

            StoredSkills = StoredSkills.OrderByDescending(item => item.Modified).ToList();

            OnSavesChanged();
        }

        private void OnSavesChanged()
        {
            StoreChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
