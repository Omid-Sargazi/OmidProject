using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.CategoryContracts.Commands
{
    public class UpdateCategoryCommand : Command
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; } 
    }

    public class UpdateCategoryCommandResponse : CommandResponse
    {
        public int Id { get; set; }
    }

}
