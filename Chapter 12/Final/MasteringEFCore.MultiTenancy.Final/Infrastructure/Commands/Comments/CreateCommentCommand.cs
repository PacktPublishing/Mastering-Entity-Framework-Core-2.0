using MasteringEFCore.MultiTenancy.Final.Core.Commands.Comments;
using MasteringEFCore.MultiTenancy.Final.Data;
using MasteringEFCore.MultiTenancy.Final.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Final.Infrastructure.Commands.Comments
{
    public class CreateCommentCommand : CommandBase, ICreateCommentCommand<int>
    {
        private readonly IConfigurationRoot _configuration;
        public CreateCommentCommand(IConfigurationRoot configuration, BlogContext context) : base(context)
        {
            _configuration = configuration;
        }

        public string Content { get; set; }
        public int PostId { get; set; }
        public string Nickname { get; set; }

        public int Handle()
        {
            int returnValue = 0;
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(
                            "INSERT INTO Person(Nickname) " +
                            "output INSERTED.ID VALUES(@Nickname)", connection))
                        {
                            sqlCommand.Parameters.AddWithValue("@FirstName", Nickname);
                            sqlCommand.Transaction = transaction;

                            int personId = (int)sqlCommand.ExecuteScalar();

                            var options = new DbContextOptionsBuilder<BlogContext>()
                                .UseSqlServer(connection)
                                .Options;

                            using (var context = new BlogContext(options))
                            {

                                Comment comment = new Comment
                                {
                                    Content = Content,
                                    PostId = PostId,
                                    CreatedAt = DateTime.Now,
                                    ModifiedAt = DateTime.Now,
                                    CommentedAt = DateTime.Now,
                                    PersonId = personId,
                                    CreatedBy = personId,
                                    ModifiedBy = personId
                                };

                                context.Database.UseTransaction(transaction);
                                context.Comments.Add(comment);
                                returnValue = context.SaveChanges();
                            }

                            transaction.Commit();

                            if (connection.State == System.Data.ConnectionState.Open)
                                connection.Close();

                            return returnValue;
                        }
                    }
                    catch (Exception exception)
                    {
                        transaction.Rollback();
                        ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
                    }
                }
            }
            return returnValue;
        }

        public async Task<int> HandleAsync()
        {
            int returnValue = 0;
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int personId = 0;
                        var person = await Context.People
                            .FirstOrDefaultAsync(item
                                => item.NickName.Equals(Nickname));

                        if (person == null)
                        {
                            using (SqlCommand sqlCommand = new SqlCommand(
                                "INSERT INTO Person(Nickname) " +
                                "output INSERTED.ID VALUES(@Nickname)", connection))
                            {
                                sqlCommand.Parameters.AddWithValue("@Nickname", Nickname);
                                sqlCommand.Transaction = transaction;

                                var inserted = await sqlCommand.ExecuteScalarAsync();
                                int.TryParse(inserted.ToString(), out personId);
                            }
                        }
                        else
                        {
                            personId = person.Id;
                        }

                        var options = new DbContextOptionsBuilder<BlogContext>()
                                .UseSqlServer(connection)
                                .Options;

                        using (var context = new BlogContext(options))
                        {

                            Comment comment = new Comment
                            {
                                Content = Content,
                                PostId = PostId,
                                CreatedAt = DateTime.Now,
                                ModifiedAt = DateTime.Now,
                                CommentedAt = DateTime.Now,
                                PersonId = personId,
                                CreatedBy = personId,
                                ModifiedBy = personId
                            };

                            context.Database.UseTransaction(transaction);
                            context.Comments.Add(comment);

                            returnValue = await context.SaveChangesAsync();
                        }

                        transaction.Commit();

                        if (connection.State == System.Data.ConnectionState.Open)
                            connection.Close();

                        return returnValue;
                    }
                    catch (Exception exception)
                    {
                        transaction.Rollback();
                        ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
                    }
                }
            }
            return returnValue;
        }
    }
}
