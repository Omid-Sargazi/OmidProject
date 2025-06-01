using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.CategoryContracts.Commands
{
    public class CreateCategoryCommand : Command
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        // public int Level { get; set; }

    }

    public class CreateCategoryCommandResponse : CommandResponse
    {
        public int Id { get; set; }
    }
}
