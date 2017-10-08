using MasteringEFCore.Transactions.Final.Core.Commands.Files;
using MasteringEFCore.Transactions.Final.Helpers;
using MasteringEFCore.Transactions.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Transactions.Final.Data;
using System.Runtime.ExceptionServices;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Transactions.Final.Infrastructure.Commands.Files
{
    public class CreateFileCommand : CommandFileBase, ICreateFileCommand<int>
    {
        private readonly DbTransaction _dbTransaction;
        public CreateFileCommand(BlogFilesContext context) : base(context)
        {
        }

        public CreateFileCommand(BlogFilesContext context, DbTransaction dbTransaction) 
            : this(context)
        {
            _dbTransaction = dbTransaction;
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
            int returnValue = 0;
            using (var transaction = _dbTransaction != null 
                ? Context.Database.UseTransaction(_dbTransaction)
                : Context.Database.BeginTransaction())
            {
                try
                {
                    AddFile();
                    returnValue = Context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
                }
            }
            return returnValue;
        }

        private void AddFile()
        {
            File file = new File()
            {
                Id = Guid.NewGuid(),
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
            int returnValue = 0;
            using (var transaction = _dbTransaction != null
                ? Context.Database.UseTransaction(_dbTransaction)
                : Context.Database.BeginTransaction())
            {
                try
                {
                    AddFile();
                    returnValue = await Context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
                }
            }
            return returnValue;
        }
    }
}
