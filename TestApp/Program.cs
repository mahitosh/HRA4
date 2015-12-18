using HRA4.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IApplicationContext app = new ApplicationContext();
            var tmp = app.ServiceContext.AdminService.GetTenants();
        }
    }
}
