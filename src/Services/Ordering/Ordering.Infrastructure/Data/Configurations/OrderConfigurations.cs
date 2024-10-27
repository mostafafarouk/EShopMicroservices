using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.Property(oi => oi.Id).HasConversion(
                orderId => orderId.Value, dbId => OrderId.of(dbId));
            builder.HasOne<Customer>()
           .WithMany()
           .HasForeignKey(oi => oi.CustomerId)
           .IsRequired();

            builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId)
            .IsRequired();

            builder.ComplexProperty(
               o => o.ShippingAddress, addressBuilder =>
               {
                   addressBuilder.Property(a => a.FirstName)
                       .HasMaxLength(50)
                       .IsRequired();

                   addressBuilder.Property(a => a.LastName)
                        .HasMaxLength(50)
                        .IsRequired();

                   addressBuilder.Property(a => a.EmailAddress)
                       .HasMaxLength(50);

                   addressBuilder.Property(a => a.AddressLine)
                       .HasMaxLength(180)
                       .IsRequired();

                   addressBuilder.Property(a => a.Country)
                       .HasMaxLength(50);

                   addressBuilder.Property(a => a.State)
                       .HasMaxLength(50);

                   addressBuilder.Property(a => a.ZipCode)
                       .HasMaxLength(5)
                       .IsRequired();
               });

            builder.ComplexProperty(
     o => o.BillingAddress, addressBuilder =>
     {
         addressBuilder.Property(a => a.FirstName)
              .HasMaxLength(50)
              .IsRequired();

         addressBuilder.Property(a => a.LastName)
              .HasMaxLength(50)
              .IsRequired();

         addressBuilder.Property(a => a.EmailAddress)
             .HasMaxLength(50);

         addressBuilder.Property(a => a.AddressLine)
             .HasMaxLength(180)
             .IsRequired();

         addressBuilder.Property(a => a.Country)
             .HasMaxLength(50);

         addressBuilder.Property(a => a.State)
             .HasMaxLength(50);

         addressBuilder.Property(a => a.ZipCode)
             .HasMaxLength(5)
             .IsRequired();
     });

            builder.ComplexProperty(
                   o => o.Payment, paymentBuilder =>
                   {
                       paymentBuilder.Property(p => p.CardName)
                           .HasMaxLength(50);

                       paymentBuilder.Property(p => p.CardNumber)
                           .HasMaxLength(24)
                           .IsRequired();

                       paymentBuilder.Property(p => p.Expiration)
                           .HasMaxLength(10);

                       paymentBuilder.Property(p => p.CVV)
                           .HasMaxLength(3);

                       paymentBuilder.Property(p => p.PaymentMethod);
                   });
            builder.Property(o => o.TotalPride);
        }
    }
}
