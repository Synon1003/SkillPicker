using SkillPicker.Persistence;
using System.Timers;
using Timer = System.Timers.Timer;

namespace SkillPicker.Model
{
    public class SkillPickerModel : ISkillPickerModel, IDisposable
    {
        private ISkillPickerDataAccess _dataAccess;
        private Random _skillGenerator;

        private SortedSet<Skill> _skills;
        private List<Skill> _practiceList;
        private List<StuntImage> _stuntImages;
        private List<String> _practiceLabels;

        private Timer _timer;
        private String _randomSkill = "";

        public String RandomSkill { get { return _randomSkill; } }
        public Boolean IsRunning { get { return _timer.Enabled; } }

        public event EventHandler<SkillsChangedEventArgs>? SkillsChanged;
        public event EventHandler<PracticeChangedEventArgs>? PracticeChanged;
        public event EventHandler<PracticeChangedEventArgs>? PracticeSkillsChanged;
        public event EventHandler<StuntImagesChangedEventArgs>? StuntImagesChanged;
        public event EventHandler<PracticeLabelsChangedEventArgs>? PracticeLabelsChanged;
        public event EventHandler? RandomSkillPicked;

        public IEnumerable<Skill> Skills { get { return _skills; } set { _skills = new SortedSet<Skill>(value); } }
        public List<StuntImage> StuntImages { get { return _stuntImages; } set { _stuntImages = new List<StuntImage>(value); } }
        public SkillPickerModel(ISkillPickerDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _skillGenerator = new Random();
            _skills = new SortedSet<Skill>(new SkillComparer());
            _practiceList = new List<Skill>();
            _stuntImages = new List<StuntImage>();
            InitPracticeLabels();

            _timer = new Timer(20);
            _timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        public void RunPicker()
        {
            if (_timer.Enabled)
                return;

            _timer.Start();
        }

        public void TakeRandomSkill()
        {
            _timer.Stop();
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            List<Skill> _randomLabelSkills = _skills.Where(s => s.Label == "Random").ToList();
            if (_randomLabelSkills.Count > 0)
            { 
                _randomSkill = _randomLabelSkills[_skillGenerator.Next(0, _randomLabelSkills.Count)].Name;
                RandomSkillPicked?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ResetSkills()
        {
            _skills = new SortedSet<Skill>();
            SkillsChanged?.Invoke(this, new SkillsChangedEventArgs(_skills));
        }

        public void CreateNewSkill(Skill skill)
        {
            if (!_skills.Any(s => s.Equals(skill)))
            {
                _skills.Add(skill);

                SkillsChanged?.Invoke(this, new SkillsChangedEventArgs(_skills));
            }
        }

        public void DeleteSkill(Skill skill)
        {
            foreach (Skill s in _skills)
            {
                if (s.Equals(skill))
                {
                    _skills.Remove(s);
                    break;
                }
            }

            SkillsChanged?.Invoke(this, new SkillsChangedEventArgs(_skills));
        }

        public void DowngradeSkill(String skillName)
        {
            foreach (Skill s in _skills)
            {
                if (s.Name.Equals(skillName))
                {
                    s.Downgrade();
                    break;
                }
            }

            SkillsChanged?.Invoke(this, new SkillsChangedEventArgs(_skills));
        }

        public void UpgradeSkill(String skillName)
        {
            foreach (Skill s in _skills)
            {
                if (s.Name.Equals(skillName))
                {
                    s.Upgrade();
                    break;
                }
            }

            SkillsChanged?.Invoke(this, new SkillsChangedEventArgs(_skills));
        }

        public async Task AddStuntImageAsync(StuntImage stuntImage, byte[] imageBytes)
        {
            if (_dataAccess == null)
                return;

            if (!_stuntImages.Any(s => s.Equals(stuntImage)))
            {
                stuntImage.ImageUrl = await _dataAccess.CreateImage(stuntImage.Filename, imageBytes);
                _stuntImages.Add(stuntImage);

                StuntImagesChanged?.Invoke(this, new StuntImagesChangedEventArgs(_stuntImages));
            }
        }

        public void DeleteStuntImage(StuntImage stuntImage)
        {
            if (_stuntImages.Count <= 1 || _dataAccess == null)
                return;

            foreach (StuntImage s in _stuntImages)
            {
                if (s.Equals(stuntImage))
                {
                    _stuntImages.Remove(s);
                    _dataAccess.DeleteImage(s.ImageUrl);
                    
                    StuntImagesChanged?.Invoke(this, new StuntImagesChangedEventArgs(_stuntImages));
                    break;
                }
            }
        }

        public void InitSkills()
        {
            if (_skills.Count == 0)
            {
                _skills.Add(new Skill { Name = "WalkUp to Hands", Label = "Prep", Type = "WarmUp" });
                _skills.Add(new Skill { Name = "Toss to Hands", Label = "Prep", Type = "WarmUp" });
                _skills.Add(new Skill { Name = "WalkUp to Platform", Label = "Platform", Type = "WarmUp" });
                _skills.Add(new Skill { Name = "Toss to Hands -> Platform", Label = "Platform", Type = "WarmUp" });
                _skills.Add(new Skill { Name = "WalkUp to Liberty", Label = "OneLeg", Type = "WarmUp" });
                _skills.Add(new Skill { Name = "Toss to Hands -> Liberty", Label = "OneLeg", Type = "WarmUp" });

                _skills.Add(new Skill { Name = "Toss to Platform", Label = "Platform", Type = "Learning" });
                _skills.Add(new Skill { Name = "Toss Liberty", Label = "OneLeg", Type = "Learning" });
                _skills.Add(new Skill { Name = "Hands Cupie", Label = "OneArm", Type = "Learning" });
                _skills.Add(new Skill { Name = "Toss Cupie", Label = "OneArm", Type = "Learning" });
                _skills.Add(new Skill { Name = "FullUp to Platform", Label = "Spin", Type = "Learning" });
                _skills.Add(new Skill { Name = "HandInHand", Label = "Inverted", Type = "Learning" });
            }

            SkillsChanged?.Invoke(this, new SkillsChangedEventArgs(_skills));
        }

        public void InitStuntImages()
        {
            if (_stuntImages.Count == 0)
            { 
                _stuntImages.Add(new StuntImage { Stunt = "Scorpion", Protagonist = "Hanó", ImageUrl = "scorpion2024.jpg", Filename = "scorpion2024.jpg" });
                _stuntImages.Add(new StuntImage { Stunt = "Hand In Hand Split", Protagonist = "Hanó", ImageUrl = "highhandstandsplit2024.jpg", Filename = "highhandstandsplit2024.png" });
                _stuntImages.Add(new StuntImage { Stunt = "Pretty Girl", Protagonist = "Elise", ImageUrl = "prettygirl2024.jpg", Filename = "prettygirl2024.jpg" });
            }

            StuntImagesChanged?.Invoke(this, new StuntImagesChangedEventArgs(_stuntImages));
        }

        public void InitPracticeLabels() 
        {
            _practiceLabels = Skill.GetLabels().GetRange(0, 9);
            _practiceLabels.AddRange(Skill.GetLabels().GetRange(1, 9));

            PracticeLabelsChanged?.Invoke(this, new PracticeLabelsChangedEventArgs(_practiceLabels));
        }

        public void SetLabels(List<String> newLabels)
        {
            for (int idx = 0; idx < newLabels.Count && idx < _practiceLabels.Count; idx++)
                _practiceLabels[idx] = newLabels[idx];
        }


        public void GeneratePractice(Int32 warmUps, Int32 learnings)
        {
            List<String> warmUpLabels = _practiceLabels.GetRange(0, 9);
            List<String> learningLabels = _practiceLabels.GetRange(9, 9);

            if (warmUps != _practiceList.Where(s => s.Type == "WarmUp").Count() ||
                learnings != _practiceList.Where(s => s.Type == "Learning").Count())
            {
                _practiceList.Clear();

                for (Int32 idx = 0; idx < warmUps; idx++)
                {
                    List<Skill> warmUpSkillsByLabel = Skills.Where(s => s.Label == warmUpLabels[idx] && s.Type == "WarmUp").ToList();
                    Int32 skillCount = warmUpSkillsByLabel.Count;

                    _practiceList.Add(new Skill
                    {
                        Label = warmUpLabels[idx],
                        Name = skillCount > 0 ? warmUpSkillsByLabel[_skillGenerator.Next(0, skillCount)].Name : "<No Skill Yet>",
                        Type = "WarmUp",
                    });
                }

                for (Int32 idx = 0; idx < learnings; idx++)
                {
                    List<Skill> learningSkillsByLabel = Skills.Where(s => s.Label == learningLabels[idx] && s.Type == "Learning").ToList();
                    Int32 skillCount = learningSkillsByLabel.Count;

                    _practiceList.Add(new Skill 
                    {
                        Label = learningLabels[idx],
                        Name = skillCount > 0 ? learningSkillsByLabel[_skillGenerator.Next(0, skillCount)].Name : "<No Skill Yet>",
                        Type = "Learning",
                    });
                }

                PracticeChanged?.Invoke(this, new PracticeChangedEventArgs(_practiceList));
            }
            else
            {
                for (Int32 idx = 0; idx < warmUps; idx++)
                {
                    List<Skill> warmUpSkillsByLabel = Skills.Where(s => s.Label == warmUpLabels[idx] && s.Type == "WarmUp").ToList();
                    Int32 skillCount = warmUpSkillsByLabel.Count;

                    _practiceList[idx].Name = skillCount > 0 ? warmUpSkillsByLabel[_skillGenerator.Next(0, skillCount)].Name : "<No Skill Yet>";
                    _practiceList[idx].Label = warmUpLabels[idx];
                }

                for (Int32 idx = 0; idx < learnings; idx++)
                {
                    List<Skill> learningSkillsByLabel = Skills.Where(s => s.Label == learningLabels[idx] && s.Type == "Learning").ToList();
                    Int32 skillCount = learningSkillsByLabel.Count;

                    _practiceList[idx + warmUps].Name = skillCount > 0 ? learningSkillsByLabel[_skillGenerator.Next(0, skillCount)].Name : "<No Skill Yet>";
                    _practiceList[idx + warmUps].Label = learningLabels[idx];
                }

                PracticeSkillsChanged?.Invoke(this, new PracticeChangedEventArgs(_practiceList));
            }
        }

        public async Task LoadSkillsAsync(String path)
        {
            if (_dataAccess == null)
                return;

            Tuple<List<String>, List<String>> dataTexts = await _dataAccess.LoadSkillsAsync(path);

            List<String> skillTexts = dataTexts.Item1;
            _skills = new SortedSet<Skill>(new SkillComparer());
            foreach (String skillText in skillTexts)
            {
                if (String.IsNullOrEmpty(skillText) || skillText.StartsWith(':'))
                    continue;

                _skills.Add(new Skill(skillText));
            }

            SkillsChanged?.Invoke(this, new SkillsChangedEventArgs(_skills));


            List<String> savedPracticeLabels = dataTexts.Item2;
            for (int idx = 0; idx < savedPracticeLabels.Count && idx < _practiceLabels.Count; idx++)
                _practiceLabels[idx] = savedPracticeLabels[idx];

            PracticeLabelsChanged?.Invoke(this, new PracticeLabelsChangedEventArgs(_practiceLabels));
        }

        public async Task SaveSkillsAsync(String path)
        {
            if (_dataAccess == null)
                return;

            List<String> skillTexts = new List<String>();
            foreach (Skill skill in _skills)
            {
                if (String.IsNullOrEmpty(skill.Name)) 
                    continue;

                skillTexts.Add(skill.ToString());
            }

            await _dataAccess.SaveSkillsAsync(path, skillTexts, _practiceLabels);
        }

        public async Task LoadImagesAsync(String path)
        {
            if (_dataAccess == null)
                return;

            List<String> imageTexts = await _dataAccess.LoadImagesAsync(path);

            _stuntImages = new List<StuntImage>();
            foreach (String imageText in imageTexts)
            {
                _stuntImages.Add(new StuntImage(imageText));
            }
        
            StuntImagesChanged?.Invoke(this, new StuntImagesChangedEventArgs(_stuntImages));
        }

        public async Task SaveImagesAsync(String path)
        {
            if (_dataAccess == null)
                return;

            List<String> imageTexts = new List<String>();
            foreach (StuntImage si in _stuntImages)
            {
                imageTexts.Add(si.ToString());
            }

            await _dataAccess.SaveImagesAsync(path, imageTexts);
        }
    }
}
