using System.ComponentModel.DataAnnotations;

namespace BookCRUD.Models
{
    public class BookViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter the title of the Book")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Please Enter the Price of the Book")]
        public double Price { get; set; }

        public string? Image { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please Enter the Author of the Book")]
        public string Author { get; set; }
    }
}
