using OmidProject.Frameworks.Contracts.Common;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.ACL.Contracts.Sms;

public interface ISmsAclService : IACLService
{
    Task<ACLResult<SmsAclOutputModel>> Send(SmsAclInputModel model);
}