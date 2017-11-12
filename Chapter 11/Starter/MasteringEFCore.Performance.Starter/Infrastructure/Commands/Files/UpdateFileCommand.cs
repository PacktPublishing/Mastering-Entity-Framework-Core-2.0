using MasteringEFCore.Performance.Starter.Core.Commands.Files;
using MasteringEFCore.Performance.Starter.Data;
using MasteringEFCore.Performance.Starter.Helpers;
using MasteringEFCore.Performance.Starter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace MasteringEFCore.Performance.Starter.Infrastructure.Commands.Files
{
    public class UpdateFileCommand : CommandFileBase, ICreateFileCommand<int>
    {
        public UpdateFileCommand(BlogFilesContext context) : base(context)
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
            UpdateFile();
            return Context.SaveChanges();
        }

        public async Task<int> HandleAsync()
        {
            UpdateFile();
            return await Context.SaveChangesAsync();
        }

        private void UpdateFile()
        {
            File file = new File()
            {
                Id = Id,
                Name = Name,
                FileName = FileName,
                Content = Content,
                Length = Length,
                ContentType = ContentType
            };

            Context.Update(file);
        }
    }
}
