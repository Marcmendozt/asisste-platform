using Asisste.ApiData.Configuration;
using Asisste.Domain.Entities;
using Asisste.Services.Abstractions.MobileAccess;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Asisste.ApiData.MobileAccess;

public sealed class SqlMobileUserProfileService : IMobileUserProfileService
{
    private readonly string connectionString;

    public SqlMobileUserProfileService(LegacySqlOptions options)
    {
        connectionString = options.ConnectionString;
    }

    public async Task<MobileUserProfile?> GetProfileAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        var normalizedUsername = username.Trim();

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand("sp_ListarUsuarioMovil", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar, 100)
        {
            Direction = ParameterDirection.Input,
            Value = normalizedUsername
        });

        command.Parameters.Add(new SqlParameter("@Clave", SqlDbType.VarChar, 255)
        {
            Direction = ParameterDirection.Input,
            Value = password
        });

        await using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken);

        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var userIdOrdinal = reader.GetOrdinal("ID_Usuario");
        var fullNameOrdinal = reader.GetOrdinal("Nombre");
        var entryTimeOrdinal = reader.GetOrdinal("HoraEntrada");
        var toleranceOrdinal = reader.GetOrdinal("Tolerancia");
        var mobileIdOrdinal = reader.GetOrdinal("ID_Movil");
        var mobileSessionOrdinal = reader.GetOrdinal("SessionMovil");

        return new MobileUserProfile(
            reader.GetInt32(userIdOrdinal),
            ReadString(reader, fullNameOrdinal),
            ReadString(reader, entryTimeOrdinal),
            reader.GetInt32(toleranceOrdinal),
            reader.GetInt32(mobileIdOrdinal),
            reader.GetBoolean(mobileSessionOrdinal));
    }

    private static string ReadString(SqlDataReader reader, int ordinal)
    {
        return reader.IsDBNull(ordinal) ? string.Empty : reader.GetString(ordinal);
    }
}