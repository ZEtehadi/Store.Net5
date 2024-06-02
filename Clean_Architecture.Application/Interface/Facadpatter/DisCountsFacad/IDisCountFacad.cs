using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Application.Services.DisCounts.Commands.AddNewDisCount;
using Clean_Architecture.Application.Services.DisCounts.Commands.ChangeStatus;
using Clean_Architecture.Application.Services.DisCounts.Commands.DeleteDisCount;
using Clean_Architecture.Application.Services.DisCounts.Commands.EditDisCount;
using Clean_Architecture.Application.Services.DisCounts.Queries.GetDisCount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Interface.Facadpatter.DisCountsFacad
{
   public interface IDisCountFacad
    {
        public AddNewDisCountService AddNewDisCountService { get; }
        public GetDisCountService GetDisCountService { get; }
        public EditDisCountService EditDisCountService { get; }
        public ChangeStatusService ChangeStatusChange { get; }
        public DeleteDisCountService DeleteDisCountService { get; }

    }
}
