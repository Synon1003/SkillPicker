namespace SkillPicker.Persistence
{
    public interface ISkillPickerDataAccess
    {
        Task<List<String>> LoadAsync(String path);

        Task SaveAsync(String path, List<String> values);
    }
}
