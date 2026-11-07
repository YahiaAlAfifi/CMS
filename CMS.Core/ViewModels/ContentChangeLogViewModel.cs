using CMS.Core.Enums;
using System;

namespace CMS.Core.ViewModels
{
    public class ContentChangeLogViewModel
    {
        public DateTime ChangeAt { get; set; }
        public ContentStatus Old { get; set; }
        public ContentStatus New { get; set; }
    }
}
