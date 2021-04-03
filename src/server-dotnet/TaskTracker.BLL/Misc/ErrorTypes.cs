using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTracker.BLL.Misc
{
    public enum ErrorTypes
    {
        APIFailure,
        LoginFailure,
        ConcurrentOperation,
        UserRoleInvalid,
        RecordExists,
        RecordNotExists,
        RecordInvalid,
        FieldOutOfRange,
        FieldTooLong,
        FieldLessThanLimit,
        FieldGreaterThanLimit,
        FieldRequired
    }
}
