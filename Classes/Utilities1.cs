using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UokSemesterSystem.Classes
{
    public static class Utilities1
    {  public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConString"].ConnectionString.ToString();
        }
    }
}