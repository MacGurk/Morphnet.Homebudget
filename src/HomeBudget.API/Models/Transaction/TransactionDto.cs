﻿using HomeBudget.API.Models.UserModels;

namespace HomeBudget.API.Models.Transaction;

/// <summary>
/// Full Transaction object
/// </summary>
public class TransactionDto
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public UserDto User { get; set; } = new();

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public bool IsSettled { get; set; }
}