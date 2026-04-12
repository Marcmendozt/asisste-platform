namespace Asisste.Domain.Entities;

public sealed record AttendanceRecord(
    DateOnly Date,
    TimeOnly? CheckInTime,
    TimeOnly? CheckOutTime,
    string? CheckInLocation,
    string? CheckOutLocation,
    string GeofenceStatus);