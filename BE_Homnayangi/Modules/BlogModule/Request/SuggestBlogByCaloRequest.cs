﻿using FluentValidation;
using System;

namespace BE_Homnayangi.Modules.BlogModule.Request
{
    public class SuggestBlogByCaloRequest
    {
        public int Age { get; set; }
        public bool IsMale { get; set; }
        public bool IsLoseWeight { get; set; }
    }
    public class SuggestBlogByCaloRequestValidator : AbstractValidator<SuggestBlogByCaloRequest>
    {
        public SuggestBlogByCaloRequestValidator()
        {
            RuleFor(x => x.Age).NotEmpty().NotNull();
        }
    }
}
