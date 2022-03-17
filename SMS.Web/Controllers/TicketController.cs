using System;
using Microsoft.AspNetCore.Mvc;
using SMS.Web.Models;
using SMS.Data.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SMS.Web.Controllers
{
    public class TicketController : BaseController
    {
        private readonly IStudentService svc;
        public TicketController()
        {
            svc = new StudentServiceDb();
        }

        // GET /ticket/index
        public IActionResult Index()
        {
            var tickets = svc.GetOpenTickets();
            return View(tickets);
        }
       
        //  POST /ticket/close/{id}
        [HttpPost]
        public IActionResult Close(int id)
        {
            // TBC - close ticket via service then check that ticket was closed
            var checker = svc.CloseTicket(id);
            var s = svc.GetTicket(id);
            var name = s.Student.Name;
            if(!checker.Active)
            {                
                 Alert($"Ticket for Student {name} removed from Active List.", AlertType.success);
            }

            // if not display a warning/error alert otherwise a success alert
           else
           {
                Alert($"Ticket for Student {name} is Active.", AlertType.danger);
           }
            return RedirectToAction(nameof(Index));
        }
       
        // GET /ticket/create
        public IActionResult Create()
        {
            // TBC - get list of students using service
            var students = svc.GetStudents();        
             // TBC - populate select list property using list of students
            var tvm = new TicketViewModel {
           
                Students = new SelectList(students,  "Id", "Name")
               
            };           

            // render blank form passing view model as a a parameter
            return View(tvm);
        }
       
        // POST /ticket/create
        [HttpPost]
        public IActionResult Create(TicketViewModel tvm)
        {
            // TBC - check if modelstate is valid and create ticket, display success alert and redirect to index
            var lists = svc.GetStudent(tvm.StudentId);
            if(ModelState.IsValid)
            {
                svc.CreateTicket(tvm.StudentId, tvm.Issue);
                
                // display suitable success alert
                Alert($"Ticket for {lists.Name} Created Successfully!", AlertType.success);
                return RedirectToAction(nameof(Index));  
            }

            // redisplay the form for editing
            return View(tvm);
        }
    }
}
