namespace MauiFilmLibrary.Model
{
    public class MovieDto
    {
        public int MovieId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateOnly? ReleaseYear { get; set; }

        public byte[]? TitleImg { get; set; }

        public List<GenerDto> GenresDto { get; set; } = new();
        public List<PersonDto> PersonDto { get; set; } = new();
    }
}