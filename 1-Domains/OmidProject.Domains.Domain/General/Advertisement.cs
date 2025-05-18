using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

namespace OmidProject.Domains.Domain.General
{
    public class Advertisement : Entity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Price { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<AdvertisementImage> AdvertisementImages { get; set; }
        
    }
}
