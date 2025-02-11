namespace SkillPicker.View;
using CommunityToolkit.Maui.Core.Platform;

public partial class EditorPage : ContentPage
{
	public EditorPage()
	{
		InitializeComponent();
	}

	private async void OnEntryUnfocused(object sender, EventArgs e)
    {
        if (KeyboardExtensions.IsSoftKeyboardShowing(nameEntry))
            await KeyboardExtensions.HideKeyboardAsync(nameEntry, default);
    }
}