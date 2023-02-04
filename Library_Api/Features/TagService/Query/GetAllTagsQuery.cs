using Library_Api.Entity;
using MediatR;

namespace Library_Api.Features.TagService.Query
{
    public class GetAllTagsQuery : IRequest<List<Tag>>
    {
    }
}