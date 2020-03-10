using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyCare
{
    public partial class Employee1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TestDBContext testDbContext = new TestDBContext();
            //Filling & Binding the gridview.  
            GridView1.DataSource = testDbContext.Employees.ToList();
            GridView1.DataBind();
        }
    }
}