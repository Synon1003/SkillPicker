namespace SkillPicker.Model
{
    public class PracticeChangedEventArgs : EventArgs
    {
        public List<Skill> PracticeList;

        public PracticeChangedEventArgs(List<Skill> practiceList)
        {
            PracticeList = practiceList;
        }
    }
}
