﻿using MasteringEFCore.Concurrencies.Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Starter.Core.Queries.Posts
{
    public interface IGetPostByIdQuery<T> : 
        IQueryHandler<Post>, IQueryHandlerAsync<Post>
    {
        int? Id { get; set; }
    }
}
