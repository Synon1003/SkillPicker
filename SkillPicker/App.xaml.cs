using SkillPicker.ViewModel;
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

            _persistence = new SkillPickerTextFileDataAccess();
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
                catch { }
            };

            window.Resumed += async (s, e) =>
            {
                try
                {
                    if (_wasStopped)
                    {
                        await _model.LoadSkillsAsync(
                            Path.Combine(FileSystem.AppDataDirectory, "SuspendedSkills"));
                        _wasStopped = false;
                    }
                }
                catch { }
            };

            window.Stopped += async (s, e) =>
            {
                try
                {
                    await _model.SaveSkillsAsync(
                        Path.Combine(FileSystem.AppDataDirectory, "SuspendedSkills"));
                    _wasStopped = true;
                }
                catch { }
            };

            return window;
        }

        #endregion
    }
}
