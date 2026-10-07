using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.UseCases.Specifications.Includes;

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
