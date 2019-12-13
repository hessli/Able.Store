﻿using System;

namespace Able.Store.Infrastructure.Dappers
{
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute()
        {

        }
        public ColumnAttribute(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
    }
}
