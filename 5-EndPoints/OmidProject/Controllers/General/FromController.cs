using System.Threading;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;
using OmidProject.Applications.Contracts.AccessibleFormContracts.Queries;
using OmidProject.Frameworks.Contracts.Markers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OmidProject.Host.Controllers.General;

[AllowAnonymous]
public class FormController : MainController
{
    public FormController(IDistributor distributor) : base(distributor)
    {
    }

    #region Accessible Form

    /// <summary>
    ///     ایجاد فرم های قابل دسترس
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("add-accessible-form")]
    [AllowAnonymous]
    public async Task<IActionResult> AddAccessibleForm(AddAccessibleFormCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<AddAccessibleFormCommand, AddAccessibleFormCommandResponse>(command,
                cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     ویرایش فرم های قابل دسترس
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("edit-accessible-form")]
    public async Task<IActionResult> EditAccessibleForm(EditAccessibleFormCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<EditAccessibleFormCommand, EditAccessibleFormCommandResponse>(command,
                cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     حذف فرم های قابل دسترس
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("remove-accessible-form")]
    public async Task<IActionResult> RemoveAccessibleForm(RemoveAccessibleFormCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<RemoveAccessibleFormCommand, RemoveAccessibleFormCommandResponse>(command,
                cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     دریافت فرم های قابل دسترس
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get-accessible-form")]
    public async Task<IActionResult> GetAccessibleForm([FromQuery] GetAccessibleFormQuery query,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PullQuery<GetAccessibleFormQuery, GetAccessibleFormQueryResponse>(query,
                cancellationToken);

        return OkApiResult(result);
    }

    #endregion


    #region Role Accessible Form

    /// <summary>
    ///     اضافه کردن فرم متصل به نقش
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("create-role-accessible-form")]
    public async Task<IActionResult> CreateRoleAccessibleForm(CreateRoleAccessibleFormCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<CreateRoleAccessibleFormCommand, CreateRoleAccessibleFormCommandResponse>(
                command, cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     اضافه کردن لیست فرم متصل به نقش
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("set-role-accessible-form-list")]
    public async Task<IActionResult> SetRoleAccessibleFormList(SetRoleAccessibleFormListCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor
                .PushCommand<SetRoleAccessibleFormListCommand, SetRoleAccessibleFormListCommandResponse>(command,
                    cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     ویرایش کردن فرم متصل به نقش
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("edit-role-accessible-form")]
    public async Task<IActionResult> EditRoleAccessibleForm(EditRoleAccessibleFormCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<EditRoleAccessibleFormCommand, EditRoleAccessibleFormCommandResponse>(command,
                cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     حذف کردن فرم متصل به نقش
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("remove-role-accessible-form")]
    public async Task<IActionResult> RemoveRoleAccessibleForm(RemoveRoleAccessibleFormCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<RemoveRoleAccessibleFormCommand, RemoveRoleAccessibleFormCommandResponse>(
                command,
                cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     دریافت کردن فرم متصل به نقش
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get-role-accessible-form")]
    public async Task<IActionResult> GetRoleAccessibleForm([FromQuery] GetRoleAccessibleFormQuery query,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PullQuery<GetRoleAccessibleFormQuery, GetRoleAccessibleFormQueryResponse>(query,
                cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     دریافت api های قابل دسترس کاربر جاری
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get-current-user-accessible-forms")]
    public async Task<IActionResult> GetCurrentUserAccessibleForms([FromQuery] GetCurrentUserAccessibleFormsQuery query,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PullQuery<GetCurrentUserAccessibleFormsQuery, GetCurrentUserAccessibleFormsQueryResponse>(
                query,
                cancellationToken);

        return OkApiResult(result);
    }

    #endregion
}