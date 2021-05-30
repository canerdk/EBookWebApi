using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EBookWebApi.DAL.Entities
{
    public class Branch
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Grade> Grades { get; set; }
    }
}
