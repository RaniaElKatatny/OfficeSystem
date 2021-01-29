using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class mvcDepartmentModel
    {

        public mvcDepartmentModel()
        {
            this.Employees = new HashSet<mvcEmployeeModel>();
            this.Managers = new HashSet<mvcManagerModel>();
        }
    
        public int Id { get; set; }

        [Required(ErrorMessage="Required Field")]
        public string name { get; set; }
    
        public virtual ICollection<mvcEmployeeModel> Employees { get; set; }
        public virtual ICollection<mvcManagerModel> Managers { get; set; }
    }
}