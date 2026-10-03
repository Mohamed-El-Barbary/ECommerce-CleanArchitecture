using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Domain.Specifications;

public sealed record OrderExpressionInfo<T>(
    Expression<Func<T, object?>> KeySelector,
    OrderType OrderType
    );

public enum OrderType
{
    OrderBy,
    OrderByDescending,
    ThenBy,
    ThenByDescending,
}