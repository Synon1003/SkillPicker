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
        event EventHandler RandomSkillPicked;
        void ResetSkills();
        void CreateNewSkill(Skill skill);
        void DeleteSkill(Skill skill);
        void DowngradeSkill(String skillName);
        void UpgradeSkill(String skillName);
        void GeneratePractice(Int32 warmUpCount, Int32 practiceCount);
        void RunPicker();
        void TakeRandomSkill();
        Task LoadSkillsAsync(String path);
        Task SaveSkillsAsync(String path);
    }
}
