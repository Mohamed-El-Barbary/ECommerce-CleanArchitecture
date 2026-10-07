using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.UseCases.Specifications.Orders;

public interface IOrderSpecificationBuilder<T>
    : ISpecificationBuilder<T> where T : BaseEntity
{
    IOrderSpecificationBuilder<T> ThenBy(Expression<Func<T, object?>> orderExpression);
    IOrderSpecificationBuilder<T> ThenByDescending(Expression<Func<T, object?>> orderExpression);
}
public interface IOrderSpecificationBuilder<T, TResult>
    : ISpecificationBuilder<T, TResult> where T : BaseEntity
{
    IOrderSpecificationBuilder<T, TResult> ThenBy(Expression<Func<T, object?>> orderExpression);
    IOrderSpecificationBuilder<T, TResult> ThenByDescending(Expression<Func<T, object?>> orderExpression);
}
