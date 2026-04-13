namespace Asisste.ApiData.Profiles;

internal sealed class ProfileStoredProcedureRow
{
    public int ID_Perfil { get; set; }

    public string Descripcion { get; set; } = string.Empty;

    public int ID_Estado { get; set; }
}