using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Domain.Entities.Commons
{
        public abstract class BaseEntity<TKey>
        {
            public TKey Id { get; set; }
            public DateTime DateTime { get; set; } = DateTime.Now;

            // ? --> UpdateTime is NullEble
            public DateTime? UpdateTime { get; set; }
            public bool IsRemoved { get; set; } = false;

            public DateTime? RemovedTime { get; set; }
            public bool IsActive { get; set; } = true;





    }
        public abstract class BaseEntity : BaseEntity<long>
        {
        }
    
}
