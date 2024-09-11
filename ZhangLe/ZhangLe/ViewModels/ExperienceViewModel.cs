using ZhangLe.ViewModels.Shared;

namespace ZhangLe.ViewModels
{
    public class ExperienceViewModel
    {
        public LayoutViewModel? Layout { get; set; }
        public string? Title { get; set; }
        public string? Background { get; set; }
        public string? Description { get; set; }
        public string? Quote { get; set; }
        public string? QuoteAuthor { get; set; }
        public List<ItemViewModel>? JobProjects { get; set; }
        public List<ExperienceSectionViewModel>? ExpSections { get; set; }
        public string? MainImage { get; set; }
    }

    public class ExperienceSectionViewModel
    {
        public QuoteViewModel? Quote { get; set; }
        public List<ImageViewModel>? Images { get; set; }
    }

    public class QuoteViewModel
    {
        public string? Text { get; set; }
        public string? Author { get; set; }
    }

    public class ImageViewModel
    {
        public string? Src { get; set; }
        public string? Alt { get; set; }
        public string? Caption { get; set; }
    }
}
