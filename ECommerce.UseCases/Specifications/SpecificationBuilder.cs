using ECommerce.Domain.Entities;
using ECommerce.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.UseCases.Specifications;

public class SpecificationBuilder<T>(Specification<T> specification) : ISpecificationBuilder<T> where T : BaseEntity
{

    protected readonly Specification<T> _specification = specification;

    public ISpecificationBuilder<T> Where(Expression<Func<T, bool>> predicate)
    {
        _specification.AddWhere(predicate);
        return this;
    }

    public IIncludableSpecificationBuilder<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigation)
    {
        var parent = _specification.AddInclude(navigation);
        return new IncludableSpecificationBuilder<T, TProperty>(_specification, parent);
    }
    public IIncludableCollectionSpecificationBuilder<T, TElement> Include<TElement>(Expression<Func<T, ICollection<TElement>>> navigation)
    {
        var parent = _specification.AddInclude(navigation);
        return new IncludableCollectionSpecificationBuilder<T, TElement>(_specification, parent);
    }

    public IOrderSpecificationBuilder<T> OrderBy(Expression<Func<T, object?>> orderExpression)
    {
        _specification.AddOrder(new OrderExpressionInfo<T>(orderExpression, OrderType.OrderBy));
        return new OrderSpecificationBuilder<T>(_specification);
    }
    public IOrderSpecificationBuilder<T> OrderByDescending(Expression<Func<T, object?>> orderExpression)
    {
        _specification.AddOrder(new OrderExpressionInfo<T>(orderExpression, OrderType.OrderByDescending));
        return new OrderSpecificationBuilder<T>(_specification);
    }

    public ISpecificationBuilder<T> Skip(int skip)
    {
        _specification.SetSkip(skip);
        return this;
    }
    public ISpecificationBuilder<T> Take(int take)
    {
        _specification.SetTake(take);
        return this;
    }

    public ISpecificationBuilder<T> AsNoTracking()
    {
        _specification.SetNoTracking();
        return this;
    }
    public ISpecificationBuilder<T> AsTracking()
    {
        _specification.SetTracking();
        return this;
    }

}

public sealed class SpecificationBuilder<T, TResult>(Specification<T, TResult> specification)
    : ISpecificationBuilder<T, TResult> where T : BaseEntity
{
    private readonly SpecificationBuilder<T> _builder = new(specification);
    private readonly Specification<T, TResult> _specification = specification;


    public ISpecificationBuilder<T, TResult> Where(Expression<Func<T, bool>> predicate)
    {
        _builder.Where(predicate);
        return this;
    }

    public IOrderSpecificationBuilder<T> OrderBy(Expression<Func<T, object?>> orderExpression)
    {
        _builder.OrderBy(orderExpression);
        return new OrderSpecificationBuilder<T>(_specification);
    }

    public IOrderSpecificationBuilder<T> OrderByDescending(Expression<Func<T, object?>> orderExpression)
    {
        _builder.OrderByDescending(orderExpression);
        return new OrderSpecificationBuilder<T>(_specification);
    }

    public ISpecificationBuilder<T, TResult> Skip(int skip)
    {
        _builder.Skip(skip);
        return this;
    }
    public ISpecificationBuilder<T, TResult> Take(int take)
    {
        _builder.Take(take);
        return this;
    }

    public ISpecificationBuilder<T, TResult> AsNoTracking()
    {
        _builder.AsNoTracking();
        return this;
    }

    public ISpecificationBuilder<T, TResult> AsTracking()
    {
        _builder.AsTracking();
        return this;
    }


    public ISpecificationBuilder<T, TResult> Select(Expression<Func<T, TResult>> selctor)
    {
        _specification.SetSelector(selctor);
        return this;
    }
    public ISpecificationBuilder<T, TResult> SelectMany(Expression<Func<T, IEnumerable<TResult>>> selctor)
    {
        _specification.SetSelectorMany(selctor);
        return this;
    }
}

public sealed class IncludableSpecificationBuilder<T, TProperty> :
    SpecificationBuilder<T>, IIncludableSpecificationBuilder<T, TProperty> where T : BaseEntity
{
    private readonly LambdaExpression _parent;
    internal IncludableSpecificationBuilder(Specification<T> specification, LambdaExpression parent)
        : base(specification)
    {
        _parent = parent;
    }

    public IIncludableSpecificationBuilder<T, TNext> ThenInclude<TNext>(Expression<Func<TProperty, TNext>> navigation)
    {
        _specification.AddThenInclude(navigation, _parent);
        return new IncludableSpecificationBuilder<T, TNext>(_specification, navigation);
    }
}
public sealed class IncludableCollectionSpecificationBuilder<T, TElement> :
    SpecificationBuilder<T>, IIncludableCollectionSpecificationBuilder<T, TElement> where T : BaseEntity
{
    private readonly LambdaExpression _parent;
    internal IncludableCollectionSpecificationBuilder(Specification<T> specification, LambdaExpression parent)
        : base(specification)
    {
        _parent = parent;
    }

    public IIncludableSpecificationBuilder<T, TNext> ThenInclude<TNext>(Expression<Func<TElement, TNext>> navigation)
    {
        _specification.AddThenInclude(navigation, _parent);
        return new IncludableSpecificationBuilder<T, TNext>(_specification, navigation);
    }
}

public sealed class OrderSpecificationBuilder<T>
    : SpecificationBuilder<T>, IOrderSpecificationBuilder<T> where T : BaseEntity
{

    internal OrderSpecificationBuilder(Specification<T> specification) : base(specification)
    {

    }
    public IOrderSpecificationBuilder<T> ThenBy(Expression<Func<T, object?>> orderExpression)
    {
        _specification.AddOrder(new OrderExpressionInfo<T>(orderExpression, OrderType.ThenBy));
        return this;
    }

    public IOrderSpecificationBuilder<T> ThenByDescending(Expression<Func<T, object?>> orderExpression)
    {
        _specification.AddOrder(new OrderExpressionInfo<T>(orderExpression, OrderType.ThenByDescending));
        return this;
    }
}