using ZhangLe.ViewModels.Shared;

namespace ZhangLe.ViewModels
{
    public class HomeViewModel
    {
        public LayoutViewModel? Layout { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? WelcomeMessage { get; set; }
        public string? AboutMe { get; set; }
        public string? Name { get; set; }
        public string? AboutDescription { get; set; }
        public string? HighschoolExperience { get; set; }
        public string? InterestsAndAwards { get; set; }
        public List<string>? Awards { get; set; }
        public string? ResumeDownload { get; set; }
        public List<ItemViewModel>? ExperiencesSection { get; set; }
        public List<ItemViewModel>? ProjectsSection { get; set; }
    }
}
