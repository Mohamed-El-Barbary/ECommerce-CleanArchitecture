using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.UseCases.Specifications.Includes;

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