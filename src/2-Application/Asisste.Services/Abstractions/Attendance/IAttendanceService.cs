namespace Asisste.Services.Abstractions.Attendance;

public interface IAttendanceService
{
    Task<AttendanceSnapshot> GetTodaySnapshotAsync(CancellationToken cancellationToken = default);

    Task<AttendanceActionResult> MarkAsync(CancellationToken cancellationToken = default);
}

public sealed record AttendanceSnapshot(
    bool HasSession,
    string UserDisplayName,
    string StatusMessage,
    string ActionLabel,
    bool CanMark,
    bool CanMarkExit,
    bool IsCompleted,
    string ExitTimeText);

public sealed record AttendanceActionResult(
    bool Success,
    string Message,
    AttendanceSnapshot Snapshot);