using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Domain.Specifications;

public sealed record IncludeExpressionInfo(
    LambdaExpression LambdaExpression,
    LambdaExpression PreviousExpression
    );
