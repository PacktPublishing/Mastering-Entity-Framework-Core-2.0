using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Performance.Starter.ViewModels
{
    public class CommentViewModel
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public string Nickname { get; set; }
    }
}
