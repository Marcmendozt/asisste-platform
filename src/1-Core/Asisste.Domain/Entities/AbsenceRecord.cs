namespace Asisste.Domain.Entities;

public sealed record AbsenceRecord(
    int UserId,
    DateOnly Date,
    string FileName);