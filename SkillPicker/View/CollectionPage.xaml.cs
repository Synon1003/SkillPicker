using CommunityToolkit.Maui.Core.Platform;

namespace SkillPicker.View
{
    public partial class CollectionPage : ContentPage
    {
        public CollectionPage()
        {
            InitializeComponent();
        }

        private async void OnSearchButtonPressed(object sender, EventArgs e)
        {
            if (KeyboardExtensions.IsSoftKeyboardShowing(searchBar))
                await KeyboardExtensions.HideKeyboardAsync(searchBar, default);
        }
    }

}
