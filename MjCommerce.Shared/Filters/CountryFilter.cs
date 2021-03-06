﻿using MjCommerce.Shared.Filters.Base;
using MjCommerce.Shared.Filters.Interfaces;
using MjCommerce.Shared.Models;
using System.Linq;

namespace MjCommerce.Shared.Filters
{
    public class CountryFilter : PagingFilter<Country>, IFilter<Country>
    {
        public string Name { get; set; }
        public string PhoneCode { get; set; }

        public IQueryable<Country> Build(IQueryable<Country> initialSet, bool applyPaging)
        {
            if (Active)
            {
                initialSet = initialSet.Where(c => c.Active == Active);
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                initialSet = initialSet.Where(c => c.Name.Contains(Name));
            }

            if (!string.IsNullOrWhiteSpace(PhoneCode))
            {
                initialSet = initialSet.Where(c => c.PhoneCode.Contains(PhoneCode));
            }

            if (applyPaging)
            {
                return Build(initialSet);
            }

            return initialSet;
        }

        public override string ToString()
        {
            return ToQueryString(GetType().GetProperties());
        }
    }
}