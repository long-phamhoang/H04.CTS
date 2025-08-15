namespace H04.Cts
{
    public static class CtsDomainErrorCodes
    {
        /* Duplicate / Conflict errors (409) */
        public const string DuplicateEntity = "Cts:Entity:Duplicate";
        public const string DuplicateCode = "Cts:Entity:DuplicateCode";
        public const string DuplicateName = "Cts:Entity:DuplicateName";

        /* Not found errors (404) */
        public const string EntityNotFound = "Cts:Entity:NotFound";
        public const string UserNotFound = "Cts:User:NotFound";
        public const string RecordNotFound = "Cts:Record:NotFound";

        /* Validation errors (400) */
        public const string InvalidInput = "Cts:Validation:InvalidInput";
        public const string MissingRequiredField = "Cts:Validation:MissingRequiredField";
        public const string InvalidDateRange = "Cts:Validation:InvalidDateRange";
        public const string InvalidStatus = "Cts:Validation:InvalidStatus";

        /* Permission / Authorization errors (403) */
        public const string UnauthorizedOperation = "Cts:Auth:UnauthorizedOperation";
        public const string Forbidden = "Cts:Auth:Forbidden";

        /* Concurrency / Locking errors */
        public const string ConcurrencyConflict = "Cts:Entity:ConcurrencyConflict";
        public const string ResourceLocked = "Cts:Entity:ResourceLocked";

        /* Business rule violations */
        public const string CannotDeleteInUse = "Cts:Entity:CannotDeleteInUse";
        public const string OperationNotAllowed = "Cts:Business:OperationNotAllowed";
    }
}