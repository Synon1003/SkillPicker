namespace SkillPicker.ViewModel
{
    public class StoredSkillsViewModel : ViewModelBase
    {
        private String _name = String.Empty;
        private DateTime _modified;

        public String Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Modified
        {
            get { return _modified; }
            set
            {
                if (_modified != value)
                {
                    _modified = value;
                    OnPropertyChanged();
                }
            }
        }

        public DelegateCommand? LoadSkillsCommand { get; set; }
        public DelegateCommand? SaveSkillsCommand { get; set; }
    }
}
