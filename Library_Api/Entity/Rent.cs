using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Api.Entity
{
    public class Rent
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Double Fee { get; set; }
    }
}
