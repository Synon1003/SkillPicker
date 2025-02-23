namespace SkillPicker.ViewModel
{
    public class SkillViewModel
    {
        public String Text { get; set; }
        public String Label { get; set; }
        public String Type { get; set; }

        public SkillViewModel()
        {
            Text = "";
            Label = "";
            Type = "Learning";
        }
    }
}
