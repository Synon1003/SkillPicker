namespace SkillPicker.Model
{
    public class SkillsChangedEventArgs : EventArgs
    {
        public List<Skill> Skills;

        public SkillsChangedEventArgs(SortedSet<Skill> skills)
        {
            Skills = new List<Skill>(skills);
        }
    }
}
