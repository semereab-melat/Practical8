using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc.Rendering;
using SMS.Data.Models;

namespace SMS.Web.Models
{
    public class TicketViewModel
    {
        // public Ticket ticketss { get; set; }
        // selectlist of students (id, name)       
        
        public SelectList Students { set; get; }
        

        // Collecting StudentId and Issue in Form
        [Required (ErrorMessage ="Please Select Student")]
        [Display(Name = "Select Student")]
        
        public int StudentId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Issue { get; set; }
    }
}
