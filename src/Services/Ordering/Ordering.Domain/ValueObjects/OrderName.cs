using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLenght = 5;
        public string Value { get; set; }
        private OrderName(string value) => Value = value;
        public static OrderName of(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length,DefaultLenght);
            return new OrderName(value);
        }
    }
}
