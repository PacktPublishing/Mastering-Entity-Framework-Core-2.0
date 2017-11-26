using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MasteringEFCore.MultiTenancy.Final.Data;
using System.IO;
using MasteringEFCore.MultiTenancy.Final.Repositories;
using MasteringEFCore.MultiTenancy.Final.Infrastructure.Queries.Files;

namespace MasteringEFCore.MultiTenancy.Final.Controllers
{
    [Produces("application/json")]
    [Route("Files")]
    public class FilesController : Controller
    {
        private BlogFilesContext _context;
        private readonly IFileRepository _fileRepository;

        public FilesController(BlogFilesContext context, IFileRepository fileRepository)
        {
            _context = context;
            _fileRepository = fileRepository;
        }

        [HttpGet("Get/{fileId}")]
        public async Task<IActionResult> Get(Guid fileId)
        {
            var file = await _fileRepository.GetSingleAsync(
                new GetFileByIdQuery(_context)
                {
                    Id = fileId
                });
            return File(file.Content, file.ContentType, file.FileName);
        }
    }
}