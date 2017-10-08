using MasteringEFCore.Transactions.Final.Core.Commands.Posts;
using MasteringEFCore.Transactions.Final.Data;
using MasteringEFCore.Transactions.Final.Helpers;
using MasteringEFCore.Transactions.Final.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Final.Infrastructure.Commands.Files
{
    public class DeleteFileCommand : CommandFileBase, ICreatePostCommand<int>
    {
        private readonly DbTransaction _dbTransaction;
        public DeleteFileCommand(BlogFilesContext context) : base(context)
        {
        }

        public DeleteFileCommand(BlogFilesContext context, DbTransaction dbTransaction) 
            : this(context)
        {
            _dbTransaction = dbTransaction;
        }

        public Guid Id { get; set; }

        public int Handle()
        {
            int returnValue = 0;
            using (var transaction = _dbTransaction != null
                ? Context.Database.UseTransaction(_dbTransaction)
                : Context.Database.BeginTransaction())
            {
                try
                {
                    DeleteFile();
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

        public async Task<int> HandleAsync()
        {
            int returnValue = 0;
            using (var transaction = _dbTransaction != null
                ? Context.Database.UseTransaction(_dbTransaction)
                : Context.Database.BeginTransaction())
            {
                try
                {
                    DeleteFile();
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

        private void DeleteFile()
        {
            var post = Context.Files.SingleOrDefault(m => m.Id == Id);
            Context.Files.Remove(post);
        }
    }
}
