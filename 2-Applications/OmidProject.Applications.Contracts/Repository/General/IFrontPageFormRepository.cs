using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Repository.General;

public interface IFrontPageFormRepository : IRepository
{
    void Add(FrontPageForm frontPageForm);
    void AddRange(List<FrontPageForm> frontPageForm);

    void Update(FrontPageForm frontPageForm);

    //void Delete(int id);
    Task<List<FrontPageForm>> GetAllAsync();
    Task<List<FrontPageForm>> GetByPaginationAsync(int skip, int take);
    bool IsExist(int id);
    Task<bool> IsExistByRouteAsync(string frontPageForm);
    Task<bool> IsExistAnotherByRouteAsync(int id, string frontPageForm);
    Task<FrontPageForm> GetById(int id);
}