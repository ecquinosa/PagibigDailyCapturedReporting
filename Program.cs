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
            CreateReport.WriteToLog("Application started");
            CreateReport createReport = new CreateReport();
            createReport.GenerateReportv2();
            CreateReport.WriteToLog("Application closed");
        }
      
    }
}
