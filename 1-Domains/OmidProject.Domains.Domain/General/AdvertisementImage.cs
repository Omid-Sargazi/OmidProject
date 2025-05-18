using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

namespace OmidProject.Domains.Domain.General
{
    public class AdvertisementImage:Entity<int>
    {
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }

        public Guid ImageId { get; set; }
        public Document Image { get; set; }
        
    }
}
