namespace Domain.Dtos;

public record GetAll(DateTime? From, DateTime? To, string? status, string? type);
