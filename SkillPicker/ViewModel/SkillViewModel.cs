namespace SkillPicker.ViewModel
{
    public class SkillViewModel
    {
        public String Text { get; set; }
        public String Label { get; set; }

        private String _type;
        public String Type { 
            get => _type;
            set 
            {
                if (_type != value)
                {
                    _type = value;
                    TypeChanged?.Invoke(this, EventArgs.Empty);
                }
            } 
        }

        public event EventHandler? TypeChanged;

        public SkillViewModel()
        {
            Text = "";
            Label = "";
            _type = "Learning";
        }
    }
}
