namespace SkillPicker.ViewModel
{
    public class StuntImageViewModel : ViewModelBase
    {
        private String _stunt;
        private String _protagonist;
        private String _filename;
        private String _imageUrl;
        private byte[] _bytes = [];

        public String Stunt
        {
            get { return _stunt; }
            set
            {
                if (_stunt != value)
                {
                    _stunt = value;
                    OnPropertyChanged();
                }
            }
        }

        public String Protagonist
        {
            get { return _protagonist; }
            set
            {
                if (_protagonist != value)
                {
                    _protagonist = value;
                    OnPropertyChanged();
                }
            }
        }

        public String Text => String.IsNullOrEmpty(Stunt) || String.IsNullOrEmpty(Protagonist) ?
            Stunt : $"{Stunt}\n({Protagonist})";

        public String Filename
        {
            get { return _filename; }
            set
            {
                if (_filename != value)
                {
                    _filename = value;
                    OnPropertyChanged();
                }
            }
        }

        public String ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                if (_imageUrl != value)
                {
                    _imageUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        public byte[] Bytes
        {
            get { return _bytes; }
            set
            {
                if (_bytes != value)
                {
                    _bytes = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
