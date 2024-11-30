namespace TaskManagement.Utils.Messages
{
    public static class Messages
    {
        public const string OkMessage = "Operation completed successfully!";

        public static string Created (string entity) => $"{entity} created successfully!";

        public static string Deleted(string entity) => $"{entity} deleted successfully!";

        public static string NotFound(string entity) => $"{entity} not found!";

        public static string RequiredProperty(string property) => $"{property} is required!";

        public static string CharacterLimit(string property, string limit) => $"The character limit for {property} is {limit}!";

        public const string UpdatedAtInvalid = "The property updatedAt cannot be less tha createdAt!";

        public const string CreatedAtInvalid = "The property createdAt cannot be in the future!";

        public static string PropertyCanBeNull(string property) => $"{property} cannot be null!"; 
    }
}
