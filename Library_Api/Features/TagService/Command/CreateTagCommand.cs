using MediatR;

namespace Library_Api.Features.TagService.Command
{
    public class CreateTagCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}