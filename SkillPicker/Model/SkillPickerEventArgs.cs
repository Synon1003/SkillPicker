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
        public List<Skill> PracticeList;

        public PracticeChangedEventArgs(List<Skill> practiceList)
        {
            PracticeList = practiceList;
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
}
