using System;
using MerchandiseService.Domain.AggregationModels.Enumerations;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public class Employee : Entity<EmployeeId>
    {
        public Employee(EmployeeId id, Email notificationEmail, ClothingSize clothingSize)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id), $"{nameof(id)} must be provided");
            NotificationEmail = notificationEmail ?? throw new ArgumentNullException(nameof(notificationEmail), 
                $"{nameof(notificationEmail)} must be provided");
            ClothingSize = clothingSize ?? throw new ArgumentNullException(nameof(clothingSize), 
                $"{nameof(clothingSize)} must be provided");
        }

        public Email NotificationEmail { get; private set; }
        public ClothingSize ClothingSize { get; private set; }

        public void ChangeClothingSize(ClothingSize newClothingSize)
        {
            ClothingSize = newClothingSize ?? 
                           throw new ArgumentNullException(nameof(newClothingSize), 
                               $"{nameof(newClothingSize)} must be provided");
        }

        public void ChangeNotificationEmail(Email notificationEmail)
        {
            NotificationEmail = notificationEmail ?? 
                                throw new ArgumentNullException(nameof(notificationEmail),
                                    $"{nameof(notificationEmail)} must be provided");
        }
    }
}