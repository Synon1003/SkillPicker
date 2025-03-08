namespace SkillPicker.Model
{
    public class SkillComparer : IComparer<Skill>
    {
        public int Compare(Skill fst, Skill sec)
        {
            return fst.Name.CompareTo(sec.Name);
        }
    }

    public class Skill
    {
        public enum SkillType
        {
            Learning,
            WarmUp,
        }
        public enum SkillLabel
        {
            Prep,
            Platform,
            OneLeg,
            OneArm,
            Spin,
            Inverted,

            Transition,
            Dismount,
            Flyerpose,

            Random,
        }

        private SkillType _type;
        private SkillLabel _label;

        public String Type
        {
            get
            {
                switch (_type)
                {
                    case SkillType.Learning: return "Learning";
                    case SkillType.WarmUp: return "WarmUp";
                    default: return "WarmUp";
                }
            }
            set
            {
                switch (value)
                {
                    case "Learning": _type = SkillType.Learning; break;
                    case "WarmUp": _type = SkillType.WarmUp; break;
                    default: _type = SkillType.WarmUp; break;
                }
            }
        }
        public String Label { 
            get 
            {
                switch (_label)
                {
                    case SkillLabel.Prep: return "Prep";
                    case SkillLabel.Platform: return "Platform";
                    case SkillLabel.OneLeg: return "OneLeg";
                    case SkillLabel.OneArm: return "OneArm";
                    case SkillLabel.Spin: return "Spin";
                    case SkillLabel.Inverted: return "Inverted";

                    case SkillLabel.Transition: return "Transition";
                    case SkillLabel.Dismount: return "Dismount";
                    case SkillLabel.Flyerpose: return "Flyerpose";

                    default: return "Random";
                }
            }
            set
            {
                switch (value)
                {
                    case "Prep": _label = SkillLabel.Prep; break;
                    case "Platform": _label = SkillLabel.Platform; break;
                    case "OneLeg": _label = SkillLabel.OneLeg; break;
                    case "OneArm": _label = SkillLabel.OneArm; break;
                    case "Spin": _label = SkillLabel.Spin; break;
                    case "Inverted": _label = SkillLabel.Inverted; break;

                    case "Transition": _label = SkillLabel.Transition; break;
                    case "Dismount": _label = SkillLabel.Dismount; break;
                    case "Flyerpose": _label = SkillLabel.Flyerpose; break;

                    default: _label = SkillLabel.Random; break;
                }
            }
        }

        public String Name { get; set; }


        public Skill(String text = "")
        {
            if (text.Contains(':'))
            {
                Name = text.Split(':')[0];
                Label = text.Split(':')[1];

                if (text.Contains(":LearningSkill"))
                    _type = SkillType.Learning;

                if (text.Contains(":WarmUpSkill"))
                    _type = SkillType.WarmUp;
            }
            else 
            {
                Name = text;
            }
        }

        public override Boolean Equals(Object obj)
        {
            return (obj is Skill skill) && 
                Name == skill.Name;
        }

        public void Downgrade()
        {
            _type = SkillType.Learning;
        }

        public void Upgrade()
        {
            _type = SkillType.WarmUp;
        }

        public static List<String> GetLabels()
        {
            return Enum.GetNames(typeof(SkillLabel)).ToList();
        }

        public static List<String> GetTypes()
        {
            return Enum.GetNames(typeof(SkillType)).ToList();
        }

        public override String ToString()
        {
            return Name + ':' + Label + (_type == SkillType.WarmUp ? ":WarmUpSkill" : "") + (_type == SkillType.Learning ? ":LearningSkill" : "");
        }
    }
}
