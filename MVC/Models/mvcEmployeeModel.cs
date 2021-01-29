using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcEmployeeModel
    {

        public mvcEmployeeModel()
        {
            this.Managers = new HashSet<mvcManagerModel>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int age { get; set; }
        public int depID { get; set; }
    
        public virtual mvcDepartmentModel Department { get; set; }
        public virtual ICollection<mvcManagerModel> Managers { get; set; }
    }
}