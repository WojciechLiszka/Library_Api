using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Api.Features.TagService.Command
{
    public class AddTagToBookCommand: IRequest
    {
        public int BookId { get; set; }
        public int TagId { get; set; }
    }
}
