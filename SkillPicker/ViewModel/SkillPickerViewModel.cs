using System.Collections.ObjectModel;
using SkillPicker.Model;

namespace SkillPicker.ViewModel
{
    public class SkillPickerViewModel : ViewModelBase
    {
        #region Fields
        private ISkillPickerModel _model;

        private SkillViewModel _skillViewModel;

        private String _searchText = "";
        private List<SkillViewModel> _searchedSkills;
        private ObservableCollection<StuntPicture> _stuntPictures;

        private Int32 _warmUps = 4;
        private Int32 _learnings = 1;
        private ObservableCollection<FieldViewModel> _warmUpFields;
        private ObservableCollection<FieldViewModel> _learningFields;

        private ObservableCollection<SkillViewModel> _warmUpSkills;
        private ObservableCollection<SkillViewModel> _learningSkills;

        #endregion

        #region Properties
        public String RandomSkill { get { return (_model.RandomSkill == "") ? "?" : _model.RandomSkill; } }

        public SkillViewModel SkillViewModel
        {
            get { return _skillViewModel; }
            set
            {
                if (_skillViewModel != value)
                {
                    _skillViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                if (String.IsNullOrEmpty(_searchText))
                    SearchSkillCommand.Execute(_searchText);
            }
        }

        public List<SkillViewModel> SearchedSkills
        {
            get { return _searchedSkills; }
            private set
            {
                if (_searchedSkills != value)
                {
                    _searchedSkills = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<StuntPicture> StuntPictures
        {
            get { return _stuntPictures; }
            private set
            {
                if (_stuntPictures != value)
                {
                    _stuntPictures = value;
                    OnPropertyChanged();
                }
            }
        }

        public Int32 WarmUps
        {
            get { return _warmUps; }
            set
            {
                if (_warmUps != value)
                {
                    _warmUps = value;
                    OnPropertyChanged();
                }
            }
        }

        public Int32 Learnings
        {
            get { return _learnings; }
            set
            {
                if (_learnings != value)
                {
                    _learnings = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<FieldViewModel> WarmUpFields
        {
            get { return _warmUpFields; }
            set
            {
                if (_warmUpFields != value)
                {
                    _warmUpFields = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<FieldViewModel> LearningFields
        {
            get { return _learningFields; }
            set
            {
                if (_learningFields != value)
                {
                    _learningFields = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<SkillViewModel> WarmUpSkills
        {
            get { return _warmUpSkills; }
            private set
            {
                if (_warmUpSkills != value)
                {
                    _warmUpSkills = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<SkillViewModel> LearningSkills
        {
            get { return _learningSkills; }
            private set
            {
                if (_learningSkills != value)
                {
                    _learningSkills = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<String> SkillLabels { get { return Skill.GetLabels(); } }
        public List<String> SkillTypes { get { return Skill.GetTypes(); } }
        #endregion

        #region Events
        public DelegateCommand GenerateCommand { get; private set; }
        public DelegateCommand SearchSkillCommand { get; private set; }
        public DelegateCommand StartStopCommand { get; private set; }
        public DelegateCommand NewSkillCommand { get; private set; }
        public DelegateCommand ResetSkillsCommand { get; private set; }
        public DelegateCommand LoadSkillsCommand { get; private set; }
        public DelegateCommand SaveSkillsCommand { get; private set; }
        public DelegateCommand DeleteSkillCommand { get; private set; }
        public DelegateCommand DowngradeSkillCommand { get; private set; }
        public DelegateCommand UpgradeSkillCommand { get; private set; }

        public event EventHandler<ApplicationMessageEventArgs>? ApplicationMessaged;
        public event EventHandler? LoadSkills;
        public event EventHandler? SaveSkills;
        #endregion


        #region Constructors
        public SkillPickerViewModel(ISkillPickerModel model) {
            _model = model;
            _model.SkillsChanged += new EventHandler<SkillsChangedEventArgs>(Model_SkillsChanged);
            _model.PracticeChanged += new EventHandler<PracticeChangedEventArgs>(Model_PracticeChanged);
            _model.PracticeSkillsChanged += new EventHandler<PracticeChangedEventArgs>(Model_PracticeSkillsChanged);
            _model.RandomSkillPicked += new EventHandler(Model_RandomSkillPicked);

            _skillViewModel = new SkillViewModel();

            _searchedSkills = new List<SkillViewModel>();
            _stuntPictures = new ObservableCollection<StuntPicture>(GetStuntPictures());

            _warmUpFields = new ObservableCollection<FieldViewModel>();
            _learningFields = new ObservableCollection<FieldViewModel>();
            
            _warmUpSkills = new ObservableCollection<SkillViewModel>();
            _learningSkills = new ObservableCollection<SkillViewModel>();

            SearchSkillCommand = new DelegateCommand(param => OnSearchSkill(param as String));
            GenerateCommand = new DelegateCommand(param => GenerateFields());
            StartStopCommand = new DelegateCommand(param => OnStartStop());
            NewSkillCommand = new DelegateCommand(param => OnNewSkill());
            ResetSkillsCommand = new DelegateCommand(param => OnResetSkills());
            LoadSkillsCommand = new DelegateCommand(param => OnLoadSkills());
            SaveSkillsCommand = new DelegateCommand(param => OnSaveSkills());
            DeleteSkillCommand = new DelegateCommand(param => OnDeleteSkill(param as SkillViewModel));
            DowngradeSkillCommand = new DelegateCommand(param => OnDowngradeSkill(param as SkillViewModel));
            UpgradeSkillCommand = new DelegateCommand(param => OnUpgradeSkill(param as SkillViewModel));
        }
        #endregion

        #region Private Methods
        private void Model_SkillsChanged(object? sender, SkillsChangedEventArgs e)
        {
            List<SkillViewModel> warmUpList = new List<SkillViewModel>();
            List<SkillViewModel> learningList = new List<SkillViewModel>();
            foreach (Skill skill in e.Skills)
            {
                if (skill.Type == "Learning")
                    learningList.Add(new SkillViewModel
                    { 
                        Text = skill.Name, 
                        Label = skill.Label, 
                        Type = skill.Type,
                    });

                else if (skill.Type == "WarmUp")
                    warmUpList.Add(new SkillViewModel
                    {
                        Text = skill.Name,
                        Label = skill.Label,
                        Type = skill.Type,
                    });
            }
            WarmUpSkills = new ObservableCollection<SkillViewModel>(warmUpList);
            LearningSkills = new ObservableCollection<SkillViewModel>(learningList);
            SearchedSkills = new List<SkillViewModel>(warmUpList.Concat(learningList));
        }

        private void Model_PracticeChanged(object? sender, PracticeChangedEventArgs e)
        {
            var practiceWarmUpList = e.PracticeList.Where(s => s.Type == "WarmUp").ToList();
            WarmUpFields.Clear();

            for (Int32 idx = 0; idx < practiceWarmUpList.Count; idx++)
            {
                WarmUpFields.Add(new FieldViewModel
                {
                    Row = idx + 1,
                    Label = practiceWarmUpList[idx].Label,
                    Skill = practiceWarmUpList[idx].Name
                });
            }

            var practiceLearningList = e.PracticeList.Where(s => s.Type == "Learning").ToList();
            LearningFields.Clear();

            for (Int32 idx = 0; idx < practiceLearningList.Count; idx++)
            {
                LearningFields.Add(new FieldViewModel 
                { 
                    Row = idx +1,
                    Label = practiceLearningList[idx].Label,
                    Skill = practiceLearningList[idx].Name
                });
            }
        }

        private void Model_PracticeSkillsChanged(object? sender, PracticeChangedEventArgs e)
        {
            var practiceWarmUpList = e.PracticeList.Where(s => s.Type == "WarmUp").ToList();
            for (Int32 idx = 0; idx < practiceWarmUpList.Count; idx++)
            {
                WarmUpFields[idx].Skill = practiceWarmUpList[idx].Name;
                WarmUpFields[idx].Label = practiceWarmUpList[idx].Label;
            }

            var practiceLearningList = e.PracticeList.Where(s => s.Type == "Learning").ToList();
            for (Int32 idx = 0; idx < practiceLearningList.Count; idx++)
            {
                LearningFields[idx].Skill = practiceLearningList[idx].Name;
                LearningFields[idx].Label = practiceLearningList[idx].Label;
            }
        }

        private void Model_RandomSkillPicked(object? sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                OnPropertyChanged(nameof(RandomSkill));
            });
        }

        private void OnSearchSkill(String searchString)
        {
            List<SkillViewModel> searchedWarmUpSkills = WarmUpSkills.Where(s => s.Text.ToLower().Contains(searchString.ToLower()) || s.Label.ToLower().Contains(searchString.ToLower())).ToList();
            List<SkillViewModel> searchedLearningSkills = LearningSkills.Where(s => s.Text.ToLower().Contains(searchString.ToLower()) || s.Label.ToLower().Contains(searchString.ToLower())).ToList();
            SearchedSkills = new List<SkillViewModel>(searchedWarmUpSkills.Concat(searchedLearningSkills));
        }
        private void GenerateFields()
        {
            _model.GeneratePractice(WarmUps, Learnings);
        }

        private void OnStartStop()
        {
            if (!_model.IsRunning)
            {
                _model.RunPicker();
            }
            else
            {
                _model.TakeRandomSkill();
            }
        }

        private void OnNewSkill()
        {
            _model.CreateNewSkill(new Skill{ 
                Name = SkillViewModel.Text.Trim(),
                Label = SkillViewModel.Label,
                Type = SkillViewModel.Type,
            });

            SkillViewModel = new SkillViewModel();
        }

        private void OnDeleteSkill(SkillViewModel skill)
        {
            _model.DeleteSkill(new Skill
            {
                Name = skill.Text,
                Label = skill.Label,
                Type = skill.Type,
            });
        }

        private void OnDowngradeSkill(SkillViewModel skill)
        {
            _model.DowngradeSkill(skill.Text);
        }

        private void OnUpgradeSkill(SkillViewModel skill)
        {
            _model.UpgradeSkill(skill.Text);
        }

        private void OnResetSkills()
        {
            _model.ResetSkills();
        }

        private void OnLoadSkills()
        {
            LoadSkills?.Invoke(this, EventArgs.Empty);
        }
        
        private void OnSaveSkills()
        {
            SaveSkills?.Invoke(this, EventArgs.Empty);
        }

        private void OnApplicationMessaged(String message, MessageType type)
        {
            ApplicationMessaged?.Invoke(this, new ApplicationMessageEventArgs { Message = message, Type = type });
        }

        private List<StuntPicture> GetStuntPictures()
        {
            List<StuntPicture> pictures = new List<StuntPicture>();

            pictures.Add(new StuntPicture{ Text = "Handstand\n(Hanó)", ImageUrl = "handstand.jpg" });
            pictures.Add(new StuntPicture{ Text = "Handstand Liberty\n(Hanó)", ImageUrl = "handstandlib.jpg" });
            pictures.Add(new StuntPicture{ Text = "Handstand Split\n(Hanó)", ImageUrl = "handstandsplit.jpg" });
            pictures.Add(new StuntPicture{ Text = "High V\n(Elise)", ImageUrl = "elise_hands.jpg" });
            pictures.Add(new StuntPicture{ Text = "Heartform\n(Elise)", ImageUrl = "elise_blokk.jpg" });
            pictures.Add(new StuntPicture{ Text = "Pretty Girl\n(Elise)", ImageUrl = "elise_prettygirl.jpg" });
            pictures.Add(new StuntPicture{ Text = "Right Liberty\n(Hanó)", ImageUrl = "jlib.jpg" });
            pictures.Add(new StuntPicture{ Text = "Left Liberty\n(Hanó)", ImageUrl = "blib.jpg" });
            pictures.Add(new StuntPicture{ Text = "Right Cupie\n(Hanó)", ImageUrl = "jcupie.jpg" });
            pictures.Add(new StuntPicture{ Text = "Left Cupie\n(Hanó)", ImageUrl = "bcupie.jpg" });
            pictures.Add(new StuntPicture{ Text = "Right Stretch\n(Hanó)", ImageUrl = "jstretch.jpg" });
            pictures.Add(new StuntPicture{ Text = "Left Stretch\n(Hanó)", ImageUrl = "bstretch.jpg" });
            pictures.Add(new StuntPicture{ Text = "Arabesk\n(Hanó)", ImageUrl = "arabesk.jpg" });
            pictures.Add(new StuntPicture{ Text = "Scorpion\n(Hanó)", ImageUrl = "scorpion.jpg" });
            pictures.Add(new StuntPicture{ Text = "Bow & Arrow\n(Hanó)", ImageUrl = "bowandarrow.jpg" });

            return pictures;
        }
        #endregion
    }
}
