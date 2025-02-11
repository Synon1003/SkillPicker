namespace SkillPicker.ViewModel
{
    public enum MessageType { Information, Error }

    public class ApplicationMessageEventArgs : EventArgs
    {
        public String? Message { get; set; }

        public MessageType Type { get; set; }
    }
}
