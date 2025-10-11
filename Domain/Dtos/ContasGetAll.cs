namespace Domain.Dtos;

public record ContasGetAll(
    string? Description,
    decimal? MinValue,
    decimal? MaxValue,
    sbyte? Month,
    short? Year,
    string? Status,
    string? Type
);
