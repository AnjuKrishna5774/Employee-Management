using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_System.Models
{
    public class EmployeeModel
    {
        public int empl_id { get; set; }
        [Required(ErrorMessage = "Enter First Name")]
        public string empl_fst_name { get; set; }
        [Required(ErrorMessage = "Enter Last Name")]
        public string empl_lst_name { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        public string empl_email { get; set; }
        [Required(ErrorMessage = "Select Date")]
        public DateTime empl_dob { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        public string empl_gender { get; set; }
        
        public string empl_password { get; set; }
        [Compare(nameof(empl_password), ErrorMessage = "Password mismatch")]
        public string empl_confirm_password { get; set; }
        
    
    }
}
