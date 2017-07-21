using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderIssues.Data
{
    public class OrderRepository
    {
        private string _connectionString;
        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Order> GetOrders()
        {
            using (var context = new OrderDBDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Order>(i => i.Issues);
                context.LoadOptions = loadOptions;
                return context.Orders.Where(o => !o.completed).ToList();
            }
        }

        public Order GetOrderById(int orderId)
        {
            using (var context = new OrderDBDataContext(_connectionString))
            {
                return context.Orders.Where(o => o.id == orderId).First();
            }
        }

        public void AddNewOrder(Order ord)
        {
            using (var context = new OrderDBDataContext(_connectionString))
            {
                context.Orders.InsertOnSubmit(ord);
                context.SubmitChanges();
            }
        }

        public IEnumerable<Issue> IssuesByOrderId(int orderId)
        {
            using (var context = new OrderDBDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Issue>(i => i.Order);
                context.LoadOptions = loadOptions;
                return context.Issues.Where(i => i.OrderId == orderId && !i.resolved).ToList();
            }
        }
        public void AddNewIssue(Issue issue)
        {
            using (var context = new OrderDBDataContext(_connectionString))
            {
                context.Issues.InsertOnSubmit(issue);
                context.SubmitChanges();
            }
        }

        public void MarkCompleted(int orderId)
        {
            using (var context = new OrderDBDataContext(_connectionString))
            {
                int completed = 1;
                context.ExecuteCommand("UPDATE Orders SET completed={0} WHERE id = {1}", completed, orderId);
            }
        }

        public void MarkResolved(int issueId)
        {
            using (var context = new OrderDBDataContext(_connectionString))
            {
                int resolved = 1;
                context.ExecuteCommand("UPDATE Issues SET resolved={0} WHERE id = {1}", resolved, issueId);
            }
        }

        
    }
}
