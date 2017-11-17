namespace ClawLibrary.Core.Enums
{
    public enum ErrorCode
    {
        FileSizeIsTooBig = 10001,
        IncorrectDate = 10002,
        TooLong = 10003,
        TooShort = 10004,
        LessThanZero = 10005,
        BelowMinimumValue = 10006,
        AboveMaximumValue = 10007,
        InvalidFormat = 10008,
        CannotBeNullOrEmpty = 10009,
        CannotBeNull = 10010,
        InvalidValue = 10011,
        FileNotFound = 10012,
        ValidationPasswordLength = 10013,
        ValidationEmailFormat = 10014,
        ValidationPasswordDoesNotMatch = 10015,
        ValidationStringLength = 10016,
        UserDoesNotExist = 10017,
        UserVerified = 10018,
        InvalidVerificationCode = 10019,
        AccountAlreadyActivatedOrBlocked = 10020,
        WrongImageFile = 10021,
        AuthorDoesNotExist = 10022,
        CategoryDoesNotExist = 10023,
        BookDoesNotExist = 10024,
        WrongStatus = 10025,
        InvalidIsbnCode = 10026,
        InternalError = 10027,
        AlreadyExist = 10028,
        PasswordResetKeyExpired = 10029
    }
}