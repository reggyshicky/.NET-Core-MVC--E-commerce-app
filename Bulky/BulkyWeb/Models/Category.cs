using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        [Key] //Data anotation, id is the primary key of our table
        public int Id { get; set; }
        [Required] //Not null
        public string Name { get; set; }
        public int DisplayOrder { get; set; } //which category should be display 1st on the page
    }
}
