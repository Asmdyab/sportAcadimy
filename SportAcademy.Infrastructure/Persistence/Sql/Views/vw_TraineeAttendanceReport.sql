

CREATE OR ALTER VIEW dbo.vw_TraineeAttendanceReport AS

WITH AttendanceData AS
(
    SELECT
        t.Id                    AS TraineeId,
        t.FirstName,
        t.LastName,
        tg.Id                   AS GroupId,
        tg.Name                 AS GroupName,
        s.Name                  AS SportName,
        b.Name                  AS BranchName,
        sd.StartDate            AS SubscriptionStartDate,
        sd.EndDate              AS SubscriptionEndDate,
        e.Id                    AS EnrollmentId,
        e.IsActive,

        -- إجمالي الحصص الفعلية اللي اتعملت للجروب ده
        (
            SELECT COUNT(*)
            FROM   SessionOccurrences so
            JOIN   GroupSchedules     gs ON so.GroupScheduleId = gs.Id
            WHERE  gs.TraineeGroupId  = tg.Id
            AND    so.Status          = 'Completed'
        ) AS TotalSessions,

        -- الحصص اللي المتدرب ده حضرها فعلاً
        (
            SELECT COUNT(*)
            FROM   Attendances a
            WHERE  a.EnrollmentId     = e.Id
            AND    a.AttendanceStatus = 'Present'
        ) AS AttendedSessions,

        -- الحصص اللي المتدرب ده غاب فيها
        (
            SELECT COUNT(*)
            FROM   Attendances a
            WHERE  a.EnrollmentId     = e.Id
            AND    a.AttendanceStatus = 'Absent'
        ) AS AbsentSessions

    FROM  Trainees           t
    JOIN  Enrollments        e   ON  e.TraineeId           = t.Id
                                AND  e.IsDeleted            = 0
    JOIN  TraineeGroups      tg  ON  tg.Id                 = e.TraineeGroupId
    JOIN  SubscriptionDetails sd ON  sd.Id                 = e.SubscriptionDetailsId
                                AND  sd.IsDeleted           = 0
    JOIN  Sports             s   ON  s.Id                  = sd.SportId
    JOIN  Branches           b   ON  b.Id                  = sd.BranchId
    WHERE t.IsDeleted = 0
)

SELECT
    TraineeId,
    FirstName,
    LastName,
    GroupId,
    GroupName,
    SportName,
    BranchName,
    SubscriptionStartDate,
    SubscriptionEndDate,
    EnrollmentId,
    IsActive,
    TotalSessions,
    AttendedSessions,
    AbsentSessions,

    -- نسبة الحضور
    CASE
        WHEN TotalSessions > 0
        THEN CAST(CAST(AttendedSessions AS FLOAT) / TotalSessions * 100 AS DECIMAL(5, 2))
        ELSE 0
    END AS AttendanceRate,

    -- نسبة الغياب
    CASE
        WHEN TotalSessions > 0
        THEN CAST(CAST(AbsentSessions AS FLOAT) / TotalSessions * 100 AS DECIMAL(5, 2))
        ELSE 0
    END AS AbsenceRate

FROM AttendanceData;