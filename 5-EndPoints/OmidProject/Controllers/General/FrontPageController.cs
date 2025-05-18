using OmidProject.Frameworks.Contracts.Markers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using OmidProject.Applications.Contracts.FrontPageFormContracts.Commands;
using OmidProject.Applications.Contracts.FrontPageFormContracts.Queries;
using Microsoft.AspNetCore.Authorization;

namespace OmidProject.Host.Controllers.General;

public class FrontPageController : MainController
{
    public FrontPageController(IDistributor distributor) : base(distributor)
    {
    }

    #region Front Page Form

    /// <summary>
    ///     اضافه کردن فرم دسترسی های فرانت
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("create-front-page-form")]
    public async Task<IActionResult> CreateFrontPageForm(CreateFrontPageFormCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<CreateFrontPageFormCommand, CreateFrontPageFormCommandResponse>(
                command, cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     ویرایش کردن فرم دسترسی های فرانت
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("edit-front-page-form")]
    public async Task<IActionResult> EditFrontPageForm(EditFrontPageFormCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<EditFrontPageFormCommand, EditFrontPageFormCommandResponse>(
                command, cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     حذف کردن فرم دسترسی های فرانت
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("remove-front-page-form")]
    public async Task<IActionResult> RemoveFrontPageForm(RemoveFrontPageFormCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<RemoveFrontPageFormCommand, RemoveFrontPageFormCommandResponse>(
                command, cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     دریافت فرم های دسترسی های فرانت
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("get-front-page-form")]
    public async Task<IActionResult> GetAccessibleForm([FromQuery] GetFrontPageFormQuery query,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PullQuery<GetFrontPageFormQuery, GetFrontPageFormQueryResponse>(query,
                cancellationToken);

        return OkApiResult(result);
    }

    #endregion

    #region Role Front Page

    /// <summary>
    ///     اضافه کردن فرم دسترسی های فرانت برای نقش
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("create-role-front-page-form")]
    public async Task<IActionResult> CreateRoleFrontPageForm(CreateRoleFrontPageFormCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<CreateRoleFrontPageFormCommand, CreateRoleFrontPageFormCommandResponse>(
                command, cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     اضافه کردن لیست فرم دسترسی های فرانت برای نقش
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("set-role-front-page-form-list")]
    public async Task<IActionResult> SetRoleFrontPageFormList(SetRoleFrontPageFormListCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<SetRoleFrontPageFormListCommand, SetRoleFrontPageFormListCommandResponse>(
                command, cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     ویرایش کردن فرم دسترسی های فرانت برای نقش
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("edit-role-front-page-form")]
    public async Task<IActionResult> EditRoleFrontPageForm(EditRoleFrontPageFormCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<EditRoleFrontPageFormCommand, EditRoleFrontPageFormCommandResponse>(
                command, cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     حذف کردن فرم دسترسی های فرانت برای نقش
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("remove-role-front-page-form")]
    public async Task<IActionResult> RemoveRoleFrontPageForm(RemoveRoleFrontPageFormCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<RemoveRoleFrontPageFormCommand, RemoveRoleFrontPageFormCommandResponse>(
                command, cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     دریافت فرم های دسترسی های فرانت
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get-role-front-page-form")]
    public async Task<IActionResult> GetRoleFrontPageForm([FromQuery] GetRoleFrontPageFormQuery query,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PullQuery<GetRoleFrontPageFormQuery, GetRoleFrontPageFormQueryResponse>(query,
                cancellationToken);

        return OkApiResult(result);
    }

    /// <summary>
    ///     دریافت فرم های قابل دسترس کاربر جاری
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("get-current-user-front-page-forms")]
    public async Task<IActionResult> GetCurrentUserFrontPageForms([FromQuery] GetCurrentUserFrontPageFormsQuery query,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PullQuery<GetCurrentUserFrontPageFormsQuery, GetCurrentUserFrontPageFormsQueryResponse>(
                query,
                cancellationToken);

        return OkApiResult(result);
    }

    #endregion
}