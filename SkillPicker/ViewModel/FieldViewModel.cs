namespace SkillPicker.ViewModel
{
    public class FieldViewModel : ViewModelBase
    {
        private String _Skill;
        private String _Label;
        
        public Int32 Row { get; set; }
        public String Skill
        {
            get { return _Skill; }
            set
            {
                if (_Skill != value)
                {
                    _Skill = value;
                    OnPropertyChanged();
                }
            }
        }
        public String Label
        {
            get { return _Label; }
            set
            {
                if (_Label != value)
                {
                    _Label = value;
                    OnPropertyChanged();
                }
            }
        }

        public FieldViewModel()
        {
            _Skill = "<Select Skill>";
            _Label = "<Select Label>";
        }
    }
}
