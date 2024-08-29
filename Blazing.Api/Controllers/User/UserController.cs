using Blazing.Application.Dto;
using Blazing.Identity.Entities;
using Blazing.Identity.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Blazing.Api.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class UserController(ILogger<UserController> logger, IUserInfrastructureRepository userInfrastructureRepository)
    : ControllerBase
{
    private readonly IUserInfrastructureRepository _userInfrastructureRepository = userInfrastructureRepository;
    private readonly ILogger<UserController> _logger = logger;


    [HttpPost]
    public async Task<ActionResult<UserCreationResult>> AddUser(IEnumerable<UserDto> usersDto,
        CancellationToken cancellationToken)
    {
        var result = await _userInfrastructureRepository.AddUsersAsync(usersDto, cancellationToken);

        _logger.LogInformation(
            "Usuários foram adicionados com sucesso. Total adicionados: {TotalAdicionado} Total com falha: {TotalFalha}",
            result.SuccessfulUsers.Count, result.FailedUsers.Length);

        return await Task.FromResult(Ok(result));
    }

    [HttpPut("Update")]
    public async Task<ActionResult<UserCreationResult>> UpdateUser([FromQuery] IEnumerable<Guid> id,
        IEnumerable<UserDto> userDtos, CancellationToken cancellationToken)
    {
        var result = await _userInfrastructureRepository.UpdateUsersAsync(id, userDtos, cancellationToken);

        _logger.LogInformation(
            "Usuários foram atualizados com sucesso. Total Atualizados: {TotalAtualizados} Total com falha: {TotalFalha}",
            result.SuccessfulUsers.Count(), result.FailedUsers.Length);

        return await Task.FromResult(Ok(result));
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult<UserCreationResult>> DeleteUsers(IEnumerable<UserDto> userDto,
        CancellationToken cancellationToken)
    {
        var result = await _userInfrastructureRepository.DeleteUsersAsync(userDto, cancellationToken);

        _logger.LogInformation(
            "Os usuários foram excluídos com sucesso. Total Usuários Deletados: {TotalDelete} Total com Falha: {TotalFalha}",
            result.SuccessfulUsers.Count(), result.FailedUsers.Length);

        return await Task.FromResult(Ok(result));
    }

    [HttpGet("Id")]
    public async Task<ActionResult<UserDto>> GetUserById(IEnumerable<Guid> id,
        CancellationToken cancellationToken)
    {
        var result = await _userInfrastructureRepository.GetUsersByIdAsync(id, cancellationToken);

        _logger.LogInformation(
            "A recuperação dos usuários foi realizada com sucesso. Identificadores: {ids}, Usuários: {emails}",
            string.Join(", ", result.Select(u => u.Id)),
            string.Join(", ", result.Select(u => u.Email)));

        return await Task.FromResult(Ok(result));
    }

    [HttpPost("Login")]
    public async Task<ActionResult<SignInResult>> LoginAsync(string email, string password, bool rememberMe,
        CancellationToken cancellationToken)
    {
        var result = await _userInfrastructureRepository.LoginAsync(email, password, rememberMe, cancellationToken);

        if (result.Succeeded)
            _logger.LogInformation("O login foi realizado com sucesso. E-mail: {email}", email);
        else if (result.IsLockedOut)
            _logger.LogWarning("Tentativa de login falhou. A conta está bloqueada. E-mail: {email}", email);
        else if (result.IsNotAllowed)
            _logger.LogWarning("Tentativa de login falhou. O login não é permitido para este usuário. E-mail: {email}",
                email);
        else if (result.RequiresTwoFactor)
            _logger.LogInformation("Tentativa de login requer autenticação de dois fatores. E-mail: {email}", email);
        else
            _logger.LogWarning("Falha no login. Credenciais inválidas. E-mail: {email}", email);

        return await Task.FromResult(Ok(result));
    }


    [HttpPost("TokenPassword")]
    public async Task<ActionResult<string>> GeneratePasswordResetToken(string email,
        CancellationToken cancellationToken)
    {
        var result = await _userInfrastructureRepository.GeneratePasswordResetTokenAsync(email, cancellationToken);

        _logger.LogInformation("O token para resetar a senha foi gerado com sucesso.");

        return await Task.FromResult(result);
    }

    [HttpPost("ResetPassword")]
    public async Task<ActionResult<IdentityResult>> ResetPassword(string email, string token, string newPassword,
        CancellationToken cancellationToken)
    {
        var result =
            await _userInfrastructureRepository.ResetPasswordAsync(email, token, newPassword, cancellationToken);

        if (result.Succeeded)
        {
            _logger.LogInformation("Senha redefinida com sucesso para o email: {email}", email);
            return await Task.FromResult(result);
        }

        _logger.LogInformation("Email {email} tentou reseta a senha sem exito. Senha {senha}", email, newPassword);

        return await Task.FromResult(result);
    }

    [HttpGet]
    public async Task<ActionResult<ApplicationUser>> GetAllUser(CancellationToken cancellationToken)
    {
        var result = await _userInfrastructureRepository.GetAllUsersAsync(cancellationToken);

        _logger.LogInformation("A recuperação dos usuários foi realizada com sucesso. Total Usuários: {TotalUsuários}",
            result.Count());

        return await Task.FromResult(Ok(result));
    }
}