namespace Sentinel.Api.Application.Common.Exceptions;

public class NotFoundException(string resource, object key)
    : Exception($"{resource} com identificador '{key}' não foi encontrado.");