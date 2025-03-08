using System.Collections.ObjectModel;
using SkillPicker.Model;

namespace SkillPicker.ViewModel
{
    public class SkillPickerViewModel : ViewModelBase
    {
        #region Fields
        private ISkillPickerModel _model;

        private SkillViewModel _skillViewModel;
        private StuntImageViewModel _stuntImageViewModel;

        private String _searchText = "";
        private List<SkillViewModel> _searchedSkills;
        private ObservableCollection<StuntImageViewModel> _stuntImages;

        private Int32 _warmUps = 3;
        private Int32 _learnings = 3;
        private ObservableCollection<FieldViewModel> _warmUpFields;
        private ObservableCollection<FieldViewModel> _learningFields;
        private ObservableCollection<String> _practiceLabels;

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

        public StuntImageViewModel StuntImageViewModel
        {
            get { return _stuntImageViewModel; }
            set
            {
                if (_stuntImageViewModel != value)
                {
                    _stuntImageViewModel = value;
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

        public ObservableCollection<StuntImageViewModel> StuntImages
        {
            get { return _stuntImages; }
            private set
            {
                if (_stuntImages != value)
                {
                    _stuntImages = value;
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

        public ObservableCollection<String> PracticeLabels
        {
            get { return _practiceLabels; }
            set
            {
                if (_practiceLabels != value)
                {
                    _practiceLabels = value;
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
        public DelegateCommand PickAnImageCommand { get; private set; }
        public DelegateCommand TakeAPictureCommand { get; private set; }
        public DelegateCommand NewImageCommand { get; private set; }
        public DelegateCommand DeleteImageCommand { get; private set; }
        public DelegateCommand AddImageToGalleryCommand { get; private set; }
        public DelegateCommand ManageLabelsCommand { get; private set; }
        public DelegateCommand SetLabelsCommand { get; private set; }
        public DelegateCommand ResetLabelsCommand { get; private set; }

        public event EventHandler? LoadSkills;
        public event EventHandler? SaveSkills;
        public event EventHandler? NewImage;
        public event EventHandler? ImageAddedToGallery;
        public event EventHandler? ManageLabels;
        public event EventHandler? SetLabels;
        #endregion


        #region Constructors
        public SkillPickerViewModel(ISkillPickerModel model) {
            _model = model;
            _model.SkillsChanged += new EventHandler<SkillsChangedEventArgs>(Model_SkillsChanged);
            _model.PracticeChanged += new EventHandler<PracticeChangedEventArgs>(Model_PracticeChanged);
            _model.PracticeSkillsChanged += new EventHandler<PracticeChangedEventArgs>(Model_PracticeSkillsChanged);
            _model.StuntImagesChanged += new EventHandler<StuntImagesChangedEventArgs>(Model_StuntImagesChanged);
            _model.PracticeLabelsChanged += new EventHandler<PracticeLabelsChangedEventArgs>(Model_PracticeLabelsChanged);
            _model.RandomSkillPicked += new EventHandler(Model_RandomSkillPicked);

            _skillViewModel = new SkillViewModel();
            _stuntImageViewModel = new StuntImageViewModel();

            _searchedSkills = new List<SkillViewModel>();

            _warmUpFields = new ObservableCollection<FieldViewModel>();
            _learningFields = new ObservableCollection<FieldViewModel>();
            _practiceLabels = new ObservableCollection<String>(new String[18]);

            _warmUpSkills = new ObservableCollection<SkillViewModel>();
            _learningSkills = new ObservableCollection<SkillViewModel>();

            StuntImages = new ObservableCollection<StuntImageViewModel>();

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
            ManageLabelsCommand = new DelegateCommand(param => OnManageLabels());
            SetLabelsCommand = new DelegateCommand(param => OnSetLabels());
            ResetLabelsCommand = new DelegateCommand(param => OnResetLabels());
            NewImageCommand = new DelegateCommand(param => OnNewImage());
            PickAnImageCommand = new DelegateCommand(param => OnPickAnImage());
            TakeAPictureCommand = new DelegateCommand(param => OnTakeAPicture());
            AddImageToGalleryCommand = new DelegateCommand(param => OnAddImageToGallery());
            DeleteImageCommand = new DelegateCommand(param => OnDeleteImage(param as StuntImageViewModel));

        }
        #endregion

        #region Model_EventHandlers
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
            var practiceWarmUpList = e.Practice.Where(s => s.Type == "WarmUp").ToList();
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

            var practiceLearningList = e.Practice.Where(s => s.Type == "Learning").ToList();
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
            var practiceWarmUpList = e.Practice.Where(s => s.Type == "WarmUp").ToList();
            for (Int32 idx = 0; idx < practiceWarmUpList.Count; idx++)
            {
                WarmUpFields[idx].Skill = practiceWarmUpList[idx].Name;
                WarmUpFields[idx].Label = practiceWarmUpList[idx].Label;
            }

            var practiceLearningList = e.Practice.Where(s => s.Type == "Learning").ToList();
            for (Int32 idx = 0; idx < practiceLearningList.Count; idx++)
            {
                LearningFields[idx].Skill = practiceLearningList[idx].Name;
                LearningFields[idx].Label = practiceLearningList[idx].Label;
            }
        }

        private void Model_StuntImagesChanged(object? sender, StuntImagesChangedEventArgs e)
        {
            StuntImages = new ObservableCollection<StuntImageViewModel>();
            foreach (StuntImage si in e.StuntImages)
            {
                StuntImages.Add(new StuntImageViewModel
                {
                    Stunt = si.Stunt,
                    Protagonist = si.Protagonist,
                    Filename = si.Filename,
                    ImageUrl = si.ImageUrl,
                });
            }
        }

        private void Model_PracticeLabelsChanged(object? sender, PracticeLabelsChangedEventArgs e)
        {
            if (PracticeLabels.Count != e.PracticeLabels.Count)
                return;

            PracticeLabels = new ObservableCollection<String>(e.PracticeLabels);
        }

        private void Model_RandomSkillPicked(object? sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                OnPropertyChanged(nameof(RandomSkill));
            });
        }
        #endregion

        #region Private Methods
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

        private void OnManageLabels()
        { 
            ManageLabels?.Invoke(this, EventArgs.Empty);
        }

        private void OnNewImage()
        {
            NewImage?.Invoke(this, EventArgs.Empty);
        }

        private void OnSetLabels()
        {
            _model.SetLabels(_practiceLabels.ToList());

            SetLabels?.Invoke(this, EventArgs.Empty);
        }

        private void OnResetLabels()
        {
            _model.InitPracticeLabels();
        }

        private async void OnPickAnImage()
        {
            FileResult? image = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Pick an Image",
                FileTypes = FilePickerFileType.Images
            });

            if (image == null)
                return;

            StuntImageViewModel.Bytes = File.ReadAllBytes(image.FullPath);
            StuntImageViewModel.Filename = image.FileName;
        }

        private async void OnTakeAPicture()
        {
            FileResult? photo = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
            { 
                Title = "Take a Picture"
            });

            if (photo == null)
                return;

            StuntImageViewModel.Bytes = File.ReadAllBytes(photo.FullPath);
            StuntImageViewModel.Filename = photo.FileName;
        }
        

        private async void OnAddImageToGallery()
        {
            if (String.IsNullOrEmpty(StuntImageViewModel.Filename) || StuntImages.Count >= 10)
                return;

            ImageAddedToGallery?.Invoke(this, EventArgs.Empty);

            await _model.AddStuntImageAsync(new StuntImage
            {
                Stunt = StuntImageViewModel.Stunt,
                Protagonist = StuntImageViewModel.Protagonist,
                Filename = StuntImageViewModel.Filename,
            }, StuntImageViewModel.Bytes);

            StuntImageViewModel = new StuntImageViewModel();
        }

        private void OnDeleteImage(StuntImageViewModel image)
        {
            if (StuntImages.Count > 1)
            {
                StuntImages.Remove(image);

                _model.DeleteStuntImage(new StuntImage
                {
                    ImageUrl = image.ImageUrl,
                    Filename = image.Filename,
                    Stunt = image.Stunt,
                    Protagonist = image.Protagonist,
                });
            }
        }
    #endregion
    }
}
