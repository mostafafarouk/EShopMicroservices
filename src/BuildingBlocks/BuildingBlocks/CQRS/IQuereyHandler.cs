using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TQuerey,TResponse>
        :IRequestHandler<TQuerey,TResponse>
        where TQuerey :IQuery<TResponse>
        where TResponse:notnull
    {
    }
}
