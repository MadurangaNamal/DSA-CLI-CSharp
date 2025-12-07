namespace practice.practiceFiles.Models;

public record ProductDto(
    Guid ProductId,
    string Name,
    string Description,
    decimal Price,
    bool IsAvailable);
