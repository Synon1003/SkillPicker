namespace SkillPicker.Persistence
{
    public class SkillPickerTextFileDataAccess : ISkillPickerDataAccess
    {
        public async Task<List<String>> LoadAsync(String path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    List<String> skillTexts = (await reader.ReadToEndAsync()).Split(",").ToList();

                    return skillTexts;

                }
            }
            catch
            {
                throw new Exception("Error occurred during reading SkillPickerData.");
            }
        }

        public async Task SaveAsync(String path, List<String> skillTexts)
        {
            if (path == null)
                throw new ArgumentNullException("path");
            if (skillTexts == null || skillTexts.Count == 0)
                throw new ArgumentNullException("skillTexts");

            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    await writer.WriteAsync(skillTexts.Aggregate((value1, value2) => value1 + "," + value2));
                }
            }
            catch
            {
                throw new Exception("Error occurred during writing SkillPickerData.");
            }
        }
    }
}
