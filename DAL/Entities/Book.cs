using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookWebApi.DAL.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? HitRate { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Guid GradeId { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}
