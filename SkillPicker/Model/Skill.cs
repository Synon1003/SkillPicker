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
            Hands,
            Block,
            OneLeg,
            OneArm,
            Full,
            Inverted,

            Transition,
            Dismount,
            Flyerpose,
            Mount,

            MountSkill,
            MountSkillFlyerpose,
            MountSkillDismount,
            MountFlyerpose,
            MountDismount,
            Skill,
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
                    case SkillLabel.Hands: return "Hands";
                    case SkillLabel.Block: return "Block";
                    case SkillLabel.OneLeg: return "OneLeg";
                    case SkillLabel.OneArm: return "OneArm";
                    case SkillLabel.Full: return "Full";
                    case SkillLabel.Inverted: return "Inverted";

                    case SkillLabel.Transition: return "Transition";
                    case SkillLabel.Dismount: return "Dismount";
                    case SkillLabel.Flyerpose: return "Flyerpose";
                    case SkillLabel.Mount: return "Mount";

                    case SkillLabel.MountSkill: return "MountSkill";
                    case SkillLabel.MountSkillFlyerpose: return "MountSkillFlyerpose";
                    case SkillLabel.MountSkillDismount: return "MountSkillDismount";
                    case SkillLabel.MountFlyerpose: return "MountFlyerpose";
                    case SkillLabel.MountDismount: return "MountDismount";
                    default: return "Skill";
                }
            }
            set
            {
                switch (value)
                {
                    case "Hands": _label = SkillLabel.Hands; break;
                    case "Block": _label = SkillLabel.Block; break;
                    case "OneLeg": _label = SkillLabel.OneLeg; break;
                    case "OneArm": _label = SkillLabel.OneArm; break;
                    case "Full": _label = SkillLabel.Full; break;
                    case "Inverted": _label = SkillLabel.Inverted; break;

                    case "Transition": _label = SkillLabel.Transition; break;
                    case "Dismount": _label = SkillLabel.Dismount; break;
                    case "Flyerpose": _label = SkillLabel.Flyerpose; break;
                    case "Mount": _label = SkillLabel.Mount; break;

                    case "MountSkill": _label = SkillLabel.MountSkill; break;
                    case "MountSkillFlyerpose": _label = SkillLabel.MountSkillFlyerpose; break;
                    case "MountSkillDismount": _label = SkillLabel.MountSkillDismount; break;
                    case "MountFlyerpose": _label = SkillLabel.MountFlyerpose; break;
                    case "MountDismount": _label = SkillLabel.MountDismount; break;
                    default: _label = SkillLabel.Skill; break;
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

        public static List<String> GetAllLabels()
        {
            return Enum.GetNames(typeof(SkillLabel)).ToList();
        }

        public static List<String> GetWarmUpLabels()
        {
            List<String> labels = Enum.GetNames(typeof(SkillLabel)).ToList();
            return labels.GetRange(0, 9);
        }

        public static List<String> GetLearningLabels()
        {
            List<String> labels = Enum.GetNames(typeof(SkillLabel)).ToList();
            return labels.GetRange(9,6);
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
