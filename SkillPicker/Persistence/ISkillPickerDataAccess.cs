namespace SkillPicker.Persistence
{
    public interface ISkillPickerDataAccess
    {
        Task<List<String>> LoadSkillsAsync(String path);
        Task SaveSkillsAsync(String path, List<String> values);
        Task<List<String>> LoadImagesAsync(String path);
        Task SaveImagesAsync(String path, List<String> imageTexts);
        void DeleteImage(String imageUrl);
        Task<String> CreateImage(String filename, byte[] imageBytes);
    }
}
