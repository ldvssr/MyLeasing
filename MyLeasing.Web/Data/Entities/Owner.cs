using System;
using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities
{
    public class Owner : IEntity
    {
        [Required] public string Document { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Fixed Phone")] public int FixedPhone { get; set; }

        [Required]
        [Display(Name = "Cell Phone")]
        public int CellPhone { get; set; }

        public string Address { get; set; }

        [Display(Name = "Owner Name")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Photo")] public Guid ImageId { get; set; }

        public User User { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? "https://myleasingwebruben.blob.core.windows.net/genericimages/noimage.png"
            : "https://myleasingbloblicinio.blob.core.windows.net/owners/" +
              ImageId;
        //     https://myleasingbloblicinio.blob.core.windows.net/owners/323aa891-7a37-4be2-af35-b0469acc2fc5

        public int Id { get; set; }
    }
}