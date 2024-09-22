﻿namespace PawFund.Domain.Abstractions.Entities;

public class DomainEntity<TKey>
{
    public virtual TKey Id { get; set; }

    /// <summary>
    /// True if domain entity has an identity
    /// </summary>
    /// <returns></returns>
    public bool IsTransient()
    {
        return Id.Equals(default(TKey));
    }
}
