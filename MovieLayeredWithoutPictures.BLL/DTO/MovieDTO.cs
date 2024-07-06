using System.ComponentModel.DataAnnotations;

namespace MovieLayeredWithoutPictures.BLL.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Поле НАЗВАНИЕ ФИЛЬМА должно быть установлено")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Поле РЕЖИССЁР ФИЛЬМА должно быть установлено")]
        public string? Director { get; set; }

        [Required(ErrorMessage = "Поле ЖАНР ФИЛЬМА должно быть установлено")]
        public string? Genre { get; set; }

        [Required(ErrorMessage = "Поле ГОД ПРОИЗВОДСТВА ФИЛЬМА должно быть установлено")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Поле КРАТКОЕ ОПИСАНИЕ ФИЛЬМА должно быть установлено")]
        public string? Description { get; set; }
    }
}
