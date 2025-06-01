using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.CategoryContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.CategoryContracts.Queries
{
    public class GetCategoryQuery : Query
    {
        public int Id { get; set; }
    }

    public class GetCategoryQueryResponse : QueryResponse
    {
        public CategoryDto Item { get; set; }
    }
}
