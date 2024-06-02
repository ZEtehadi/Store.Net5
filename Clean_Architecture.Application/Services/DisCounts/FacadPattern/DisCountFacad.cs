using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Application.Interface.Facadpatter.DisCountsFacad;
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

namespace Clean_Architecture.Application.Services.DisCounts.FacadPattern
{
   public class DisCountFacad:IDisCountFacad
    {
        private readonly IDataBaseContext _context;
        public DisCountFacad(IDataBaseContext context)
        {
            _context = context;
        }



        private AddNewDisCountService _addNewDisCountService;
        public AddNewDisCountService AddNewDisCountService
        {
            get
            {
                return _addNewDisCountService = _addNewDisCountService ?? new AddNewDisCountService(_context);
            }
        } 
        
        
        private GetDisCountService _getDisCountService;
        public GetDisCountService GetDisCountService
        {
            get
            {
                return _getDisCountService = _getDisCountService ?? new GetDisCountService(_context);
            }
        } 
        
        
        
        private EditDisCountService _editDisCountService;
        public EditDisCountService EditDisCountService
        {
            get
            {
                return _editDisCountService = _editDisCountService ?? new EditDisCountService(_context);
            }
        }
        
        
        
        private ChangeStatusService _changeStatusService;
        public ChangeStatusService ChangeStatusChange
        {
            get
            {
                return _changeStatusService = _changeStatusService ?? new ChangeStatusService(_context);
            }
        }
        
        
        private DeleteDisCountService _deleteDisCountService;
        public DeleteDisCountService DeleteDisCountService
        {
            get
            {
                return _deleteDisCountService = _deleteDisCountService ?? new DeleteDisCountService(_context);
            }
        }

    }
}
