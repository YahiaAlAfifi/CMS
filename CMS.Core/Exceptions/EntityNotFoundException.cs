using System;

namespace CMS.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
            : base("Item not Found")
        {
        }
    }
}
