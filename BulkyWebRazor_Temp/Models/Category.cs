using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyWebRazor_Temp.Models
{
    public class Category
    {
        [Key] //Data anotation, id is the primary key of our table
        public int Id { get; set; }
        [Required] //Not null
        [DisplayName("Category Name")]
        [MaxLength(30)] //validation
        public string Name { get; set; }
        [DisplayName("Display Order")] //Data anotation for the client side UI , the name DisplayOrder will have a space in btn; Display Order
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")] //validation
        public int DisplayOrder { get; set; } //which category should be display 1st on the page
    }
}
