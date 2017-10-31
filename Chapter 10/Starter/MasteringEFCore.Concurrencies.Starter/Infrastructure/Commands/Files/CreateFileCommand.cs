using MasteringEFCore.Concurrencies.Starter.Core.Commands.Files;
using MasteringEFCore.Concurrencies.Starter.Helpers;
using MasteringEFCore.Concurrencies.Starter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Concurrencies.Starter.Data;
using System.Runtime.ExceptionServices;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Concurrencies.Starter.Infrastructure.Commands.Files
{
    public class CreateFileCommand : CommandFileBase, ICreateFileCommand<int>
    {
        public CreateFileCommand(BlogFilesContext context) : base(context)
        {
        }

        public Guid Id { get; set; }
        public string ContentType { get; set; }
        public string ContentDisposition { get; set; }
        public byte[] Content { get; set; }
        public long Length { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }

        public int Handle()
        {
            AddFile();
            return Context.SaveChanges();
        }

        private void AddFile()
        {
            Id = Id.Equals(Guid.Empty) ? Guid.NewGuid() : Id;
            File file = new File()
            {
                Id = Id,
                Name = Name,
                FileName = FileName,
                Content = Content,
                Length = Length,
                ContentType = ContentType
            };

            Context.Add(file);
        }

        public async Task<int> HandleAsync()
        {
            AddFile();
            return await Context.SaveChangesAsync();
        }
    }
}
