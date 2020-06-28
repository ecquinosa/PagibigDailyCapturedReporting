using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_PagIBIG
{
    public static class Program 

    {
        static void Main(string[] args)
        {
            CreateReport createReport = new CreateReport();
            createReport.GenerateReportv2();
        }
      
    }
}
