using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasteringEFCore.MultiTenancy.Starter.Models
{
    public class File : IFormFile
    {
        public Guid Id { get; set; }
        public string ContentType { get; set; }

        public string ContentDisposition { get; set; }
        public byte[] Content { get; set; }

        [NotMapped]
        public IHeaderDictionary Headers { get; set; }

        public long Length { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public void CopyTo(Stream target)
        {
            throw new NotImplementedException();
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Stream OpenReadStream()
        {
            throw new NotImplementedException();
        }
    }
}
