using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkoutApp.Entities;

namespace WorkoutApp.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(100,ErrorMessage = "FirstName cannot be longer that 100 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(100, ErrorMessage = "LastName cannot be longer that 100 characters")]
        public string LastName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Birthday is required")]
        public DateTime Birthday { get; set; }

        [Column(TypeName = "nvarchar(1)")]
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        public ICollection<Workout> Workouts { get; set; }
    }
}
