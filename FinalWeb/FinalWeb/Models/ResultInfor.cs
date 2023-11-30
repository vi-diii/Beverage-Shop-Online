using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWeb.Models
{
    public class ResultInfor
    {
      
            public uint CustomerID { get; set; }
            public string ContactName { get; set; }
            public uint ProductID { get; set; }
            public string ProductName { get; set; }
         
            public float Score { get; set; }
            public string Decision { get; set; }
        

    }
}
