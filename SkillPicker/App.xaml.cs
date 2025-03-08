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
                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals("Error occurred during reading Skills."))
                    {
                        _model.InitSkills();
                        _model.InitPracticeLabels();
                    }
                }

                try
                {
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
                if (_wasStopped)
                {
                    try
                    {
                        await _model.LoadSkillsAsync(
                            Path.Combine(FileSystem.AppDataDirectory, "SuspendedSkills"));
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Equals("Error occurred during reading Skills."))
                        { 
                            _model.InitSkills();
                            _model.InitPracticeLabels();
                        }
                    }

                    try
                    {
                        await _model.LoadImagesAsync(
                            Path.Combine(FileSystem.AppDataDirectory, "SuspendedImages"));
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Equals("Error occurred during reading Images."))
                            _model.InitStuntImages();
                    }

                    _wasStopped = false;
                }
            };

            window.Stopped += async (s, e) =>
            {
                try
                {
                    await _model.SaveSkillsAsync(
                        Path.Combine(FileSystem.AppDataDirectory, "SuspendedSkills"));
                }
                catch { }

                try
                {
                    await _model.SaveImagesAsync(
                        Path.Combine(FileSystem.AppDataDirectory, "SuspendedImages"));
                }
                catch { }

                _wasStopped = true;
            };

            return window;
        }

        #endregion
    }
}
