namespace H04.Cts
{
    public static class CtsDomainErrorCodes
    {
        /* Duplicate / Conflict errors (409) */
        public const string DuplicateEntity = "CTS:Entity:Duplicate";
        public const string DuplicateCode = "CTS:Entity:DuplicateCode";
        public const string DuplicateName = "CTS:Entity:DuplicateName";

        /* Not found errors (404) */
        public const string EntityNotFound = "CTS:Entity:NotFound";
        public const string UserNotFound = "CTS:User:NotFound";
        public const string RecordNotFound = "CTS:Record:NotFound";

        /* Validation errors (400) */
        public const string InvalidInput = "CTS:Validation:InvalidInput";
        public const string MissingRequiredField = "CTS:Validation:MissingRequiredField";
        public const string InvalidDateRange = "CTS:Validation:InvalidDateRange";
        public const string InvalidStatus = "CTS:Validation:InvalidStatus";

        /* Permission / Authorization errors (403) */
        public const string UnauthorizedOperation = "CTS:Auth:UnauthorizedOperation";
        public const string Forbidden = "CTS:Auth:Forbidden";

        /* Concurrency / Locking errors */
        public const string ConcurrencyConflict = "CTS:Entity:ConcurrencyConflict";
        public const string ResourceLocked = "CTS:Entity:ResourceLocked";

        /* Business rule violations */
        public const string CannotDeleteInUse = "CTS:Entity:CannotDeleteInUse";
        public const string OperationNotAllowed = "CTS:Business:OperationNotAllowed";
    }
}