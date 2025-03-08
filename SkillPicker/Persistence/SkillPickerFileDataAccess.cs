namespace SkillPicker.Persistence
{
    public class SkillPickerFileDataAccess : ISkillPickerDataAccess
    {
        public async Task<Tuple<List<String>, List<String>>> LoadSkillsAsync(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String skillsData = await reader.ReadToEndAsync();
                    if (String.IsNullOrEmpty(skillsData))
                        throw new Exception("Empty file.");

                    String[] data = skillsData.Split(";");
                    List<String> skillTexts = data[0].Split(",").ToList();
                    List<String> labelTexts = data[1].Split(",").ToList();

                    return new Tuple<List<String>,List<String>>(skillTexts, labelTexts);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred during reading Skills.");
            }
        }

        public async Task SaveSkillsAsync(String path, List<String> skillTexts, List<String> labelTexts)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    await writer.WriteAsync(skillTexts.Aggregate((value1, value2) => value1 + "," + value2));
                    await writer.WriteAsync(';');
                    await writer.WriteAsync(labelTexts.Aggregate((value1, value2) => value1 + "," + value2));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred during writing Skills.");
            }
        }

        public async Task<List<String>> LoadImagesAsync(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String imagesData = await reader.ReadToEndAsync();
                    if (String.IsNullOrEmpty(imagesData))
                        throw new Exception("Empty file.");

                    List<String> imageTexts = imagesData.Split(",").ToList();

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
            try
            {
                string filePath = Path.Combine(FileSystem.AppDataDirectory, imageUrl);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred during deleting the Image.");
            }
        }

        public async Task<String> CreateImage(String filename, byte[] imageBytes)
        {
            try
            {
                string filePath = Path.Combine(FileSystem.AppDataDirectory, filename);

                if (!File.Exists(filePath))
                {
                    await File.WriteAllBytesAsync(filePath, imageBytes);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred during creating the Image.");
            }
        }
    }
}
