using OrderIssues.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderIssues.Models
{
    public class IssuesByOrderIdModelView
    {
        public IEnumerable<Issue> issue { get; set; }
        public Order order { get; set; }
    }
}