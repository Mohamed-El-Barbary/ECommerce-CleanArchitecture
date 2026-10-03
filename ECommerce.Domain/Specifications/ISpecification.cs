using ECommerce.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerce.Domain.Specifications;

public interface ISpecification<T> where T : BaseEntity
{
    IReadOnlyList<Expression<Func<T, bool>>> WhereExpressions { get; }
    IReadOnlyList<Expression<Func<T, object>>> Includes { get; }
    IReadOnlyList<IncludeExpressionInfo> IncludeExpressions { get; }
    IReadOnlyList<OrderExpressionInfo<T>> OrderExpressions { get; }

    int? Skip {  get; }
    int? Take { get; }
    bool IsPagingEnabled { get; }
    bool IsTrackingEnabled { get; }
}

public interface ISpecification<T, TResult> where T : BaseEntity
{
    Expression<Func<T, TResult>>? Selector { get; }
    Expression<Func<T, IEnumerable<TResult>>>? SelectorMany { get; }
}