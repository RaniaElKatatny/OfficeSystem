using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcManagerModel
    {
        public int Id { get; set; }
        public int empID { get; set; }
        public int depID { get; set; }

        public virtual mvcDepartmentModel Department { get; set; }
        public virtual mvcEmployeeModel Employee { get; set; }
    }
}