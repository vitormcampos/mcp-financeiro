namespace FinanceiroBackend.Dtos;

public record GetAll(DateOnly? From, DateOnly? To, string? status, string? type);