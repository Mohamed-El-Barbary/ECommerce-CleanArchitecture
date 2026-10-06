using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.UseCases.Specifications;

public interface ISpecificationBuilder<T> where T : BaseEntity
{
    ISpecificationBuilder<T> Where(Expression<Func<T, bool>> predicate);

    // Order
    IOrderSpecificationBuilder<T> OrderBy(Expression<Func<T, object?>> orderExpression);
    IOrderSpecificationBuilder<T> OrderByDescending(Expression<Func<T, object?>> orderExpression);

    // inclide
    IIncludableSpecificationBuilder<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigation);
    IIncludableCollectionSpecificationBuilder<T, TElement> Include<TElement>(
        Expression<Func<T, ICollection<TElement>>> navigation);

    ISpecificationBuilder<T> Skip(int skip);
    ISpecificationBuilder<T> Take(int take);

    ISpecificationBuilder<T> AsNoTracking();
    ISpecificationBuilder<T> AsTracking();
}

public interface IOrderSpecificationBuilder<T>
    : ISpecificationBuilder<T> where T : BaseEntity
{
    IOrderSpecificationBuilder<T> ThenBy(Expression<Func<T, object?>> orderExpression);
    IOrderSpecificationBuilder<T> ThenByDescending(Expression<Func<T, object?>> orderExpression);
}

public interface IIncludableSpecificationBuilder<T, TProperty>
    : ISpecificationBuilder<T> where T : BaseEntity
{
    IIncludableSpecificationBuilder<T, TNext> ThenInclude<TNext>(
        Expression<Func<TProperty, TNext>> navigation);
}

public interface IIncludableCollectionSpecificationBuilder<T, TElement>
    : ISpecificationBuilder<T> where T : BaseEntity
{
    IIncludableSpecificationBuilder<T, TNext> ThenInclude<TNext>(
        Expression<Func<TElement, TNext>> navigation);
}

public interface ISpecificationBuilder<T, TResult> where T : BaseEntity
{
    ISpecificationBuilder<T, TResult> Where(Expression<Func<T, bool>> predicate);

    // Order
    IOrderSpecificationBuilder<T> OrderBy(Expression<Func<T, object?>> orderExpression);
    IOrderSpecificationBuilder<T> OrderByDescending(Expression<Func<T, object?>> orderExpression);

    ISpecificationBuilder<T, TResult> Skip(int skip);
    ISpecificationBuilder<T, TResult> Take(int take);

    ISpecificationBuilder<T, TResult> AsNoTracking();
    ISpecificationBuilder<T, TResult> AsTracking();

    ISpecificationBuilder<T, TResult> Select(Expression<Func<T, TResult>> selctor);
    ISpecificationBuilder<T, TResult> SelectMany(Expression<Func<T, IEnumerable<TResult>>> selctor);
}