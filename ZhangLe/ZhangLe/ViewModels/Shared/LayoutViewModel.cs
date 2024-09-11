namespace ZhangLe.ViewModels.Shared
{
    public class LayoutViewModel
    {
        public string? Home { get; set; }
        public string? About { get; set; }
        public string? Experiences { get; set; }
        public string? Projects { get; set; }
        public string? Resume { get; set; }
        public string? ResumeFile { get; set; }
        public string? Discover { get; set; }
        public List<SubNavItemViewModel>? SubNav { get; set; }

        public string? Address { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }
        public string? Author { get; set; }
        public string? Rights { get; set; }
    }

    public class SubNavItemViewModel
    {
        public string? Name { get; set; }
        public string? Link { get; set; }
    }
}
