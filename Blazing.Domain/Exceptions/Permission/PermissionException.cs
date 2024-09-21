using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable All

namespace Blazing.Domain.Exceptions.Permission
{
    public static class PermissionException
    {
        public class PermissionAlreadyExistsException(string message) : DomainException(message)
        {
            public static PermissionAlreadyExistsException FromExistingId(string? id)
            {
                return new PermissionAlreadyExistsException(
                    $"Identificador da permissão já existe: {string.Join(", ", id)}");
            }

            public static PermissionAlreadyExistsException FromExistingIds(IEnumerable<Guid> id)
            {
                return new PermissionAlreadyExistsException(
                    $"Identificadores das permissões já existem: {string.Join(", ", id)}");
            }

            public static PermissionAlreadyExistsException FromExistingName(string name)
            {
                return new PermissionAlreadyExistsException(
                    $"Nome da permissão já existe: {string.Join(", ", name)}");
            }

            public static PermissionAlreadyExistsException FromExistingNames(IEnumerable<string> name)
            {
                return new PermissionAlreadyExistsException(
                    $"Nomes das permissões já existe,: {string.Join(", ", name)}");
            }
        }

        public class PermissionNotFoundException(string message) : DomainException(message)
        {
            public static PermissionNotFoundException NotFoundPermissions(IEnumerable<Entities.Permission> message)
            {
                return new PermissionNotFoundException(
                    $"Permissões não foram encontrados. Identificador:" +
                    $" {string.Join(", ", message.Select(u => u.Id))}," +
                    $" Nome: {string.Join(", ", message.Select(u => u.Name))}");
            }
        }

        public class UpdatePermissionNotFoundException(string message ) : DomainException(message)
        {
            public static UpdatePermissionNotFoundException CreateForPermissionsNotFound(
                IEnumerable<Entities.Permission> permissionsNotFound)
            {
                var invalidPermissionIds = permissionsNotFound.Select(p => p.Id);

                var notFoundIds = string.Join(", ", invalidPermissionIds);
                var notFoundNames = string.Join(", ", permissionsNotFound.Select(p => p.Name));

                var message = $"Lista de permissões para alteração não foram encontradas ou não correspondem às permissões cadastradas. " +
                              $"Identificadores: {notFoundIds}. " +
                              $"Nomes: {notFoundNames}";

                return new UpdatePermissionNotFoundException(message);
            }

            public static UpdatePermissionNotFoundException CreateForNoUpdatesDetected(
                IEnumerable<Entities.Permission> permissionsNotFound)
            {
                var invalidUpdatedPermissionIds = permissionsNotFound.Select(p => p.Id);

                var notUpdatedFoundIds = string.Join(", ", invalidUpdatedPermissionIds);
                var notUpdatedFoundNames = string.Join(", ", permissionsNotFound.Select(p => p.Name));

                var message = $"Não foram encontrado nenhuma alteração na lista de permissões para atualização. " +
                              $"Identificadores: {notUpdatedFoundIds}. " +
                              $"Nomes: {notUpdatedFoundNames}";

                return new UpdatePermissionNotFoundException(message);
            }
        }
    }
}
