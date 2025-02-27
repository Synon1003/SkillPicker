namespace SkillPicker.Persistence
{
    public class SkillPickerFileDataAccess : ISkillPickerDataAccess
    {
        public async Task<List<String>> LoadSkillsAsync(String path)
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
            catch (Exception ex)
            {
                throw new Exception("Error occurred during reading Skills.");
            }
        }

        public async Task SaveSkillsAsync(String path, List<String> skillTexts)
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
            catch (Exception ex)
            {
                throw new Exception("Error occurred during writing Skills.");
            }
        }

        public async Task<List<String>> LoadImagesAsync(String path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    List<String> imageTexts = (await reader.ReadToEndAsync()).Split(",").ToList();

                    return imageTexts;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred during reading Images.");
            }
        }

        public async Task SaveImagesAsync(String path, List<String> imageTexts)
        {
            if (path == null)
                throw new ArgumentNullException("path");
            if (imageTexts == null || imageTexts.Count == 0)
                throw new ArgumentNullException("imageTexts");

            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    await writer.WriteAsync(imageTexts.Aggregate((value1, value2) => value1 + "," + value2));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred during writing Images.");
            }
        }

        public void DeleteImage(String imageUrl)
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, imageUrl);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<String> CreateImage(String filename, byte[] imageBytes)
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filePath))
            {
                await File.WriteAllBytesAsync(filePath, imageBytes);
            }

            return filePath;
        }
    }
}
