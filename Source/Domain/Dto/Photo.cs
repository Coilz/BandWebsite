namespace Ewk.BandWebsite.Domain.Dto
{
    public class Photo
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string WebUrl { get; set; }

        public string LargeUrl { get; set; }
        public int? LargeWidth { get; set; }
        public int? LargeHeight { get; set; }

        public string MediumUrl { get; set; }
        public int? MediumWidth { get; set; }
        public int? MediumHeight { get; set; }

        public string SmallUrl { get; set; }
        public int? SmallWidth { get; set; }
        public int? SmallHeight { get; set; }

        public string OriginalUrl { get; set; }
        public int OriginalWidth { get; set; }
        public int OriginalHeigth { get; set; }
    }
}