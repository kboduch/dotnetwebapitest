namespace core_web_api_fundamentals.api.Database;

public class NotFound<T>(Guid id) : Exception($"Resource {typeof(T)} with id {id} not found");