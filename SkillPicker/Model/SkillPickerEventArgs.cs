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

    public class PracticeChangedEventArgs : EventArgs
    {
        public List<Skill> Practice;

        public PracticeChangedEventArgs(List<Skill> practice)
        {
            Practice = practice;
        }
    }

    public class StuntImagesChangedEventArgs : EventArgs
    {
        public List<StuntImage> StuntImages;

        public StuntImagesChangedEventArgs(List<StuntImage> stuntImages)
        {
            StuntImages = new List<StuntImage>(stuntImages);
        }
    }

    public class PracticeLabelsChangedEventArgs : EventArgs
    {
        public List<String> PracticeLabels;

        public PracticeLabelsChangedEventArgs(List<String> practiceLabels)
        {
            PracticeLabels = practiceLabels;
        }
    }
}
