using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

namespace OmidProject.Domains.Domain.General
{
    public class Category : Entity<int>
    {
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        public List<Category> Child { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Level { get; set; }
        public List<Advertisement> Advertisements { get; set; }

    }
}
