﻿namespace Blazing.Api.Erros.ControllerException
{
    public class NotFoundException(string message) : Exception(message) { }
    public class BadRequestException(string message) : Exception(message) { }
    public class UnauthorizedAccessExceptions(string message) : Exception(message) { }
    public class InternalServerErrorException(string message) : Exception(message) { }
    public class ConflictException(string message) : Exception(message) { }

}
