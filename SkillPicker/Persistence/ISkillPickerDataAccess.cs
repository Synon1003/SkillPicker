namespace SkillPicker.Persistence
{
    public interface ISkillPickerDataAccess
    {
        Task<Tuple<List<String>, List<String>>> LoadSkillsAsync(String path);
        Task SaveSkillsAsync(String path, List<String> skills, List<String> labels);
        Task<List<String>> LoadImagesAsync(String path);
        Task SaveImagesAsync(String path, List<String> imageTexts);
        void DeleteImage(String imageUrl);
        Task<String> CreateImage(String filename, byte[] imageBytes);
    }
}
