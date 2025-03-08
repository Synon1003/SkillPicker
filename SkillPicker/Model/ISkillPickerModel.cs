namespace SkillPicker.Model
{
    public interface ISkillPickerModel
    {
        IEnumerable<Skill> Skills { get; set; }
        String RandomSkill { get; }
        Boolean IsRunning { get; }
        event EventHandler<SkillsChangedEventArgs>? SkillsChanged;
        event EventHandler<PracticeChangedEventArgs>? PracticeChanged;
        event EventHandler<PracticeChangedEventArgs>? PracticeSkillsChanged;
        event EventHandler<StuntImagesChangedEventArgs>? StuntImagesChanged;
        event EventHandler<PracticeLabelsChangedEventArgs>? PracticeLabelsChanged;
        event EventHandler RandomSkillPicked;
        void ResetSkills();
        void CreateNewSkill(Skill skill);
        void DeleteSkill(Skill skill);
        void DowngradeSkill(String skillName);
        void UpgradeSkill(String skillName);
        void GeneratePractice(Int32 warmUpCount, Int32 practiceCount);
        void RunPicker();
        void TakeRandomSkill();
        void SetLabels(List<String> newLabels);
        Task LoadSkillsAsync(String path);
        Task SaveSkillsAsync(String path);
        Task LoadImagesAsync(String path);
        Task SaveImagesAsync(String path);
        Task AddStuntImageAsync(StuntImage stuntImage, byte[] imageBytes);
        void DeleteStuntImage(StuntImage stuntImage);
        void InitSkills();
        void InitStuntImages();
        void InitPracticeLabels();
    }
}
