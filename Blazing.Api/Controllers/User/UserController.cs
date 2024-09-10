using Blazing.Application.Dto;
using Blazing.Domain.Exceptions.User;
using Blazing.Identity.Entities;
using Blazing.Identity.Interface;
using Blazing.Identity.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Blazing.Api.Controllers.User;

#region UserControllerEndpoint


[Route("api/[controller]")]
[ApiController]
public class UserController(ILogger<UserController> logger, IUserInfrastructureRepository userInfrastructureRepository)
    : ControllerBase
{
    private readonly IUserInfrastructureRepository _userInfrastructureRepository = userInfrastructureRepository;
    private readonly ILogger<UserController> _logger = logger;


    /// <summary>
    /// Adds a collection of users to the repository.
    /// </summary>
    /// <param name="usersDto">The collection of userDto objects representing the users to be added.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>An HTTP 201 Created response with no content.</returns>
    /// <remarks>
    /// This method requires the user to be authenticated.
    /// </remarks>
    //[Authorize]
    [HttpPost]
    public async Task<ActionResult> AddUser(IEnumerable<UserDto> usersDto,
        CancellationToken cancellationToken)
    {
        var result = await _userInfrastructureRepository.AddUsersAsync(usersDto, cancellationToken);

        _logger.LogInformation(
            "Usuários foram adicionados com sucesso. Total adicionados: {TotalAdicionado} Total com falha: {TotalFalha}",
            result.SuccessfulUsers.Count, result.FailedUsers.Length);

        return Created();
    }

    /// <summary>
    /// Updates users in the repository asynchronously.
    /// </summary>
    /// <param name="userDto">An enumerable collection of UserDto objects representing the users to be updated.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>An HTTP 204 NoContent response with no content.</returns>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result is an ActionResult.
    /// </returns>
    [Authorize]
    [HttpPut("Update")]
    public async Task<ActionResult> UpdateUser(IEnumerable<UserDto> userDto, CancellationToken cancellationToken)
    {
        var result = await _userInfrastructureRepository.UpdateUsersAsync(userDto, cancellationToken);

        _logger.LogInformation(
            "Usuários foram atualizados com sucesso. Total Atualizados: {TotalAtualizados} Total com falha: {TotalFalha}",
            result.SuccessfulUsers.Count, result.FailedUsers.Length);

        return NoContent();
    }


    /// <summary>
    /// Deletes users from the repository asynchronously.
    /// </summary>
    /// <param name="id">An enumerable collection of user IDs to be deleted.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>An HTTP 204 NoContent response with no content.</returns>
    /// <remarks>
    /// This method requires the user to be authenticated and have the "admin" role.
    /// </remarks>
    [Authorize(Roles = "admin")]
    [HttpDelete("Delete")]
    public async Task<ActionResult> DeleteUsers(IEnumerable<string> id,
        CancellationToken cancellationToken)
    {
        var result = await _userInfrastructureRepository.DeleteUsersAsync(id, cancellationToken);

        _logger.LogInformation(
            "Os usuários foram excluídos com sucesso. Total Usuários Deletados: {TotalDelete} Total com Falha: {TotalFalha}",
            result.SuccessfulUsers.Count, result.FailedUsers.Length);

        return NoContent();
    }



    /// <summary>
    /// Retrieves a collection of users by their IDs.
    /// </summary>
    /// <param name="id">The IDs of the users to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>An ActionResult containing the retrieved users.</returns>
    /// <remarks>
    /// This method requires the user to be authenticated.
    /// </remarks>
    [Authorize]
    [HttpGet("id")]
    public async Task<ActionResult<UserDto>> GetUserById([FromQuery]IEnumerable<Guid> id,
        CancellationToken cancellationToken)
    {
        var result = await _userInfrastructureRepository.GetUsersByIdAsync(id, cancellationToken);

        _logger.LogInformation(
            "A recuperação dos usuários foi realizada com sucesso. Identificadores: {ids}, Usuários: {emails}",
            string.Join(", ", result.Select(u => u.Id)),
            string.Join(", ", result.Select(u => u.Email)));

        return Ok(result);
    }


    /// <summary>
    /// Retrieves all users from the database.
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <param name="page"></param>
    /// <returns>An asynchronous task that represents the operation. The task result contains an action result indicating the success of the operation.
    /// The result contains a list of all users.</returns>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ApplicationUser>> GetAllUser([FromQuery]int page, int pageSize, CancellationToken cancellationToken)
    {
        if (pageSize > 50)
            pageSize = 50;

        var result = await _userInfrastructureRepository.GetAllUsersAsync(page, pageSize, cancellationToken);

        _logger.LogInformation("A recuperação dos usuários foi realizada com sucesso. Total Usuários: {TotalUsuários}",
            result.Count());

        return Ok(result);
    }

    /// <summary>
    /// Asynchronously handles the login request.
    /// </summary>
    /// <param name="login.LoginIdentifier ">The email of the user.</param>
    /// <param name="login.password">The password of the user.</param>
    /// <param name="login.rememberMe">A boolean indicating whether the user wants to be remembered.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An ActionResult containing the SignInResult of the login attempt.</returns>
    [HttpPost("Login")]
    public async Task<ActionResult<SignInResult>> LoginAsync(Login login,
        CancellationToken cancellationToken)
    {
        if (login is null)
            return BadRequest();

        var result = await _userInfrastructureRepository.LoginAsync(login, cancellationToken);

        switch (result)
        {
            case { Succeeded: true }:
                if (!string.IsNullOrEmpty(login.LoginIdentifier) && login.LoginIdentifier.Contains("@"))
                {
                    _logger.LogInformation("O login foi realizado com sucesso. E-mail: {email}", login.LoginIdentifier);
                    return Ok(new { status = "success", message = "Login bem-sucedido." });
                }
                else
                {
                    _logger.LogInformation("O login foi realizado com sucesso. Nome do usuário: {userName}", login.LoginIdentifier );
                    return Ok(new { status = "success", message = "Login bem-sucedido." });
                }

            case { IsNotAllowed: true }:
                _logger.LogWarning("Tentativa de login falhou. O login não é permitido para este usuário. E-mail: {email}", login.LoginIdentifier );
                return Unauthorized(new { status = "error", message = "Login não permitido para este usuário." });

            case { RequiresTwoFactor: true }:
                _logger.LogInformation("Tentativa de login requer autenticação de dois fatores. E-mail: {email}", login.LoginIdentifier );
                return Unauthorized(new { status = "2fa_required", message = "Autenticação de dois fatores é necessária." });

            case { Succeeded: false }:
                if (!string.IsNullOrEmpty(login.LoginIdentifier ) && login.LoginIdentifier .Contains("@"))
                {
                    _logger.LogWarning("Tentativa de login falhou. Credenciais inválidas. E-mail: {email}", login.LoginIdentifier );
                    return Unauthorized(new { status = "error", message = "Credenciais inválidas." });
                }
                else
                {
                    _logger.LogWarning("Tentativa de login falhou. Credenciais inválidas. Nome do usuário: {email}", login.LoginIdentifier );
                    return Unauthorized(new { status = "error", message = "Credenciais inválidas." });
                }

            default:
                _logger.LogWarning("Falha no login. Credenciais inválidas. E-mail: {email}", login.LoginIdentifier );
                return Unauthorized(new { status = "error", message = "Credenciais inválidas." });
        }

    }

    /// <summary>
    /// Generates a password reset token for the specified email.
    /// </summary>
    /// <param name="email">The email for which to generate the password reset token.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The generated password reset token.</returns>
    [HttpPost("TokenPassword")]
    public async Task<ActionResult<string>> GeneratePasswordResetToken(string email,
        CancellationToken cancellationToken)
    {
        var result = await _userInfrastructureRepository.GeneratePasswordResetTokenAsync(email, cancellationToken);

        _logger.LogInformation("O token para redefinir a senha foi gerado com sucesso.");

        return Ok(result);
    }

    /// <summary>
    /// Resets the password for a user.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <param name="token">The password reset token.</param>
    /// <param name="newPassword">The new password for the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An ActionResult containing the IdentityResult of the password reset attempt.</returns>
    [HttpPost("ResetPassword")]
    public async Task<ActionResult<IdentityResult>> ResetPassword(string email, string token, string newPassword,
        CancellationToken cancellationToken)
    {
        var result =
            await _userInfrastructureRepository.ResetPasswordAsync(email, token, newPassword, cancellationToken);

        if (result.Succeeded)
        {
            _logger.LogInformation("Senha redefinida com sucesso para o email: {email}", email);
            return Ok(result);
        }

        _logger.LogInformation("Email {email} tentou redefinir a senha sem exito. Senha {senha}", email, newPassword);

        return NotFound(result);
    }
}
#endregion