using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EBookWebApi.DAL.Entities
{
    public class Grade
    {        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
