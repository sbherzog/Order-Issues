using OrderIssues.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderIssues.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Order> order { get; set; }
    }
}