namespace Asisste.Services.Abstractions.Absences;

public interface IAbsenceService
{
    Task<AbsenceSnapshot> GetTodaySnapshotAsync(CancellationToken cancellationToken = default);

    Task<AbsenceActionResult> SubmitAsync(AbsenceAttachment attachment, CancellationToken cancellationToken = default);
}

public sealed record AbsenceAttachment(
    string FileName,
    string ContentType,
    byte[] Content);

public sealed record AbsenceSnapshot(
    bool HasSession,
    bool AlreadySubmittedToday,
    string StatusMessage);

public sealed record AbsenceActionResult(
    bool Success,
    string Message,
    AbsenceSnapshot Snapshot);