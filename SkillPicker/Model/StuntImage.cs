namespace SkillPicker.Model
{
    public class StuntImage
    {
        public String Stunt { get; set; }
        public String Protagonist { get; set; }
        public String Filename { get; set; }
        public String ImageUrl { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj is StuntImage si) &&
                Filename == si.Filename;
        }

        public StuntImage(String text = "")
        {
            if (text.Contains(':'))
            { 
                String[] dataParts = text.Split(':');
                Stunt = dataParts[0];
                Protagonist = dataParts[1];
                Filename = dataParts[2];
                ImageUrl = dataParts[3];
            }
        }

        public override String ToString()
        {
            if (String.IsNullOrEmpty(Filename))
                return "";

            return Stunt + ':' + Protagonist + ':' + Filename + ':' + ImageUrl;
        }
    }
}
