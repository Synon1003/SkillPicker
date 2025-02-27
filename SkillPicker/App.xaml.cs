using SkillPicker.Model;
using SkillPicker.Persistence;

namespace SkillPicker
{
    public partial class App : Application
    {
        
        private ISkillPickerDataAccess _persistence;
        private ISkillPickerModel _model;

        private bool _wasStopped = false;

        public App()
        {
            InitializeComponent();

            _persistence = new SkillPickerFileDataAccess();
            _model = new SkillPickerModel(_persistence);

            MainPage = new AppShell(_model);
        }

        #region Application life-cycle methods

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = base.CreateWindow(activationState);

            window.Created += async (s, e) =>
            {
                try
                {
                    await _model.LoadSkillsAsync(
                        Path.Combine(FileSystem.AppDataDirectory, "SuspendedSkills"));

                    await _model.LoadImagesAsync(
                        Path.Combine(FileSystem.AppDataDirectory, "SuspendedImages"));
                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals("Error occurred during reading Images."))
                        _model.InitStuntImages();
                }
            };

            window.Resumed += async (s, e) =>
            {
                try
                {
                    if (_wasStopped)
                    {
                        await _model.LoadSkillsAsync(
                            Path.Combine(FileSystem.AppDataDirectory, "SuspendedSkills"));

                        await _model.LoadImagesAsync(
                            Path.Combine(FileSystem.AppDataDirectory, "SuspendedImages"));
                        _wasStopped = false;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals("Error occurred during reading Images."))
                        _model.InitStuntImages();
                }
            };

            window.Stopped += async (s, e) =>
            {
                try
                {
                    await _model.SaveSkillsAsync(
                        Path.Combine(FileSystem.AppDataDirectory, "SuspendedSkills"));
                    await _model.SaveImagesAsync(
                        Path.Combine(FileSystem.AppDataDirectory, "SuspendedImages"));
                    _wasStopped = true;
                }
                catch { }
            };

            return window;
        }

        #endregion
    }
}
