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

        [Required]
        public double Price { get; set; }
    }
}
