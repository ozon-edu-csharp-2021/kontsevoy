using System;
using System.Diagnostics.CodeAnalysis;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public class Employee : Entity<EmployeeId>
    {
        public Employee(EmployeeId id, Email notificationEmail, ClothingSize clothingSize)
        {
            Id = id;
            NotificationEmail = notificationEmail;
            ClothingSize = clothingSize;
        }

        public Email NotificationEmail { get; private set; }
        public ClothingSize ClothingSize { get; private set; }

        public void ChangeClothingSize([NotNull] ClothingSize newClothingSize)
        {
            ClothingSize = newClothingSize ?? 
                           throw new ArgumentNullException(nameof(newClothingSize), 
                               $"{nameof(newClothingSize)} must be provided");
        }

        public void ChangeNotificationEmail([NotNull] Email notificationEmail)
        {
            NotificationEmail = notificationEmail ?? 
                                throw new ArgumentNullException(nameof(notificationEmail),
                                    $"{nameof(notificationEmail)} must be provided");
        }
    }
}