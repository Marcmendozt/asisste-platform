using Asisste.ApiData.Persistence;
using Asisste.Domain.Entities;
using Asisste.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Asisste.ApiData.Profiles;

public sealed class EfProfileRepository : IProfileRepository
{
    private readonly LegacyAsissteDbContext dbContext;

    public EfProfileRepository(LegacyAsissteDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Profile>> ListAsync(CancellationToken cancellationToken = default)
    {
        var rows = await LoadRowsAsync(cancellationToken);
        return rows.Select(Map).ToArray();
    }

    public async Task<Profile?> GetByIdAsync(int profileId, CancellationToken cancellationToken = default)
    {
        var rows = await LoadRowsAsync(cancellationToken);
        var row = rows.FirstOrDefault(profile => profile.ID_Perfil == profileId);
        return row is null ? null : Map(row);
    }

    public async Task<Profile?> GetByDescriptionAsync(string description, CancellationToken cancellationToken = default)
    {
        var normalizedDescription = description.Trim();
        var rows = await LoadRowsAsync(cancellationToken);

        var row = rows.FirstOrDefault(profile =>
            string.Equals(profile.Descripcion, normalizedDescription, StringComparison.OrdinalIgnoreCase));

        return row is null ? null : Map(row);
    }

    public async Task<Profile?> CreateAsync(Profile profile, CancellationToken cancellationToken = default)
    {
        var descriptionParameter = new SqlParameter("@Descripcion", SqlDbType.VarChar, 100)
        {
            Direction = ParameterDirection.Input,
            Value = profile.Description
        };

        var stateParameter = new SqlParameter("@ID_Estado", SqlDbType.Int)
        {
            Direction = ParameterDirection.Input,
            Value = profile.IsActive ? 1 : 0
        };

        var existsParameter = new SqlParameter("@EXISTE", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        await dbContext.Database.ExecuteSqlRawAsync(
            "EXEC sp_PerfilAgregar @Descripcion, @ID_Estado, @EXISTE OUTPUT",
            new object[]
            {
                descriptionParameter,
                stateParameter,
                existsParameter
            },
            cancellationToken);

        if (ReadOutputInt(existsParameter) == 1)
        {
            return null;
        }

        return await GetByDescriptionAsync(profile.Description, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Profile profile, CancellationToken cancellationToken = default)
    {
        var profileIdParameter = new SqlParameter("@ID_Perfil", SqlDbType.Int)
        {
            Direction = ParameterDirection.Input,
            Value = profile.Id
        };

        var descriptionParameter = new SqlParameter("@Descripcion", SqlDbType.VarChar, 100)
        {
            Direction = ParameterDirection.Input,
            Value = profile.Description
        };

        var affectedRows = await dbContext.Database.ExecuteSqlRawAsync(
            "EXEC sp_PerfilEditar @ID_Perfil, @Descripcion",
            new object[]
            {
                profileIdParameter,
                descriptionParameter
            },
            cancellationToken);

        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int profileId, CancellationToken cancellationToken = default)
    {
        var profileIdParameter = new SqlParameter("@ID_Perfil", SqlDbType.Int)
        {
            Direction = ParameterDirection.Input,
            Value = profileId
        };

        var affectedRows = await dbContext.Database.ExecuteSqlRawAsync(
            "EXEC sp_PerfilEliminar @ID_Perfil",
            new object[]
            {
                profileIdParameter
            },
            cancellationToken);

        return affectedRows > 0;
    }

    private async Task<List<ProfileStoredProcedureRow>> LoadRowsAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Database
            .SqlQueryRaw<ProfileStoredProcedureRow>("EXEC sp_PerfilListar")
            .ToListAsync(cancellationToken);
    }

    private static Profile Map(ProfileStoredProcedureRow row)
    {
        return new Profile(
            row.ID_Perfil,
            row.Descripcion,
            row.ID_Estado == 1);
    }

    private static int ReadOutputInt(SqlParameter parameter)
    {
        return parameter.Value is int value ? value : 0;
    }
}