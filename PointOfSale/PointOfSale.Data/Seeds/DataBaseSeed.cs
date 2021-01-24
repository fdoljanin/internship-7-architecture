using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Data.Enums;

namespace PointOfSale.Data.Seeds
{
    public static class DataBaseSeed
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Offer>()
                .HasData(new List<Offer>
                    {
                    new Offer
                    {
                        Id = 1,
                        Type = OfferType.Item,
                        IsActive = true,
                        Name = "Chocolate bar",
                        Price = 1.49m,
                        Quantity = 23
                    },
                    new Offer
                    {
                        Id = 2,
                        Type = OfferType.Service,
                        IsActive = true,
                        Name = "Nail painting",
                        Price = 39.99m,
                    },
                    new Offer
                    {
                        Id = 3,
                        Type = OfferType.Rent,
                        IsActive = true,
                        Name = "E-bike",
                        Price = 70.00m,
                        Quantity = 5
                    },
                    new Offer
                    {
                        Id = 4,
                        Type = OfferType.Rent,
                        IsActive = true,
                        Name = "Magazine",
                        Price = 3.00m,
                        Quantity = 300
                    },
                    new Offer
                    {
                        Id = 5,
                        Type = OfferType.Item,
                        IsActive = true,
                        Name = "White bread",
                        Price = 0.99m,
                        Quantity = 50
                    },
                    new Offer
                    {
                        Id = 6,
                        Type = OfferType.Item,
                        IsActive = true,
                        Name = "Brown bread",
                        Price = 1.19m,
                        Quantity = 19
                    },
                    new Offer
                    {
                        Id = 7,
                        Type = OfferType.Service,
                        IsActive = true,
                        Name = "Haircut",
                        Price = 8.00m,
                    },
                    new Offer
                    {
                        Id = 8,
                        Type = OfferType.Item,
                        IsActive = true,
                        Name = "Pixel 5",
                        Price = 699.00m,
                        Quantity = 7
                    },
                    new Offer
                    {
                        Id = 9,
                        Type = OfferType.Item,
                        IsActive = true,
                        Name = "Smart Fridge",
                        Price = 1800.00m,
                        Quantity = 3
                    },
                    new Offer
                    {
                        Id = 10,
                        Type = OfferType.Rent,
                        IsActive = true,
                        Name = "Library access",
                        Price = 4.00m,
                        Quantity = 100
                    },
                    new Offer
                    {
                        Id = 11,
                        Type = OfferType.Service,
                        IsActive = true,
                        Name = "Skin cleaning",
                        Price = 24.99m,
                    },
                    new Offer
                    {
                        Id = 12,
                        Type = OfferType.Item,
                        IsActive = true,
                        Name = "Introduction to Algorithms, book",
                        Price = 80.00m,
                        Quantity = 15
                    }
                    }
                );

            builder.Entity<Category>()
                .HasData(new List<Category>
                    {
                    new Category
                    {
                        Id = 1,
                        Name = "Food"
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Technology"
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "Literature"
                    },
                    new Category
                    {
                        Id = 4,
                        Name = "Household"
                    }
                    }
                );

            builder.Entity<OfferCategory>()
                .HasData(new List<OfferCategory>
                    {
                    new OfferCategory
                    {
                    Id = 1,
                    CategoryId = 1,
                    OfferId = 1
                    },
                    new OfferCategory
                    {
                        Id = 2,
                        CategoryId = 1,
                        OfferId = 5
                    },
                    new OfferCategory
                    {
                        Id = 3,
                        CategoryId = 1,
                        OfferId = 6
                    },
                    new OfferCategory
                    {
                        Id = 4,
                        CategoryId = 2,
                        OfferId = 3
                    },
                    new OfferCategory
                    {
                        Id = 5,
                        CategoryId = 2,
                        OfferId = 8
                    },
                    new OfferCategory
                    {
                        Id = 6,
                        CategoryId = 2,
                        OfferId = 9
                    },
                    new OfferCategory
                    {
                        Id = 7,
                        CategoryId = 3,
                        OfferId = 10
                    },
                    new OfferCategory
                    {
                        Id = 8,
                        CategoryId = 3,
                        OfferId = 12
                    },
                    new OfferCategory
                    {
                        Id = 10,
                        CategoryId = 4,
                        OfferId = 9
                    }
                    });

            builder.Entity<Employee>()
                .HasData(new List<Employee>
                    {
                        new Employee
                        {
                            Id = 1,
                            FirstName = "Zara",
                            LastName = "Fisher",
                            Pin = "19493419882",
                            WorkStart = 6,
                            WorkEnd = 14
                        },
                        new Employee
                        {
                            Id = 2,
                            FirstName = "Sean",
                            LastName = "Hess",
                            Pin = "24823185487",
                            WorkStart = 8,
                            WorkEnd = 18
                        },
                        new Employee
                        {
                            Id = 3,
                            FirstName = "Eliza",
                            LastName = "Martinez",
                            Pin = "83689476125",
                            WorkStart = 16,
                            WorkEnd = 22
                        }
                    }
                );

            builder.Entity<Customer>()
                .HasData(new List<Customer>
                    {
                    new Customer
                    {
                        Id = 1,
                        FirstName = "Owen",
                        LastName = "Cole",
                        Pin = "42161657377"
                    },
                    new Customer
                    {
                        Id = 2,
                        FirstName = "Ali",
                        LastName = "Solomon",
                        Pin = "34148898582"
                    },
                    new Customer
                    {
                        Id = 3,
                        FirstName = "Megan",
                        LastName = "Hodge",
                        Pin = "24335495767"
                    },
                    new Customer
                    {
                        Id = 4,
                        FirstName = "Jamie",
                        LastName = "Witt",
                        Pin = "00484037984"
                    }
                    }
                );

            builder.Entity<ArticleBill>()
                .HasData(new List<ArticleBill>
                    {
                        new ArticleBill
                        {
                            Id = 1,
                            OfferId = 1,
                            Quantity = 3,
                            BillId = 1
                        },
                        new ArticleBill
                        {
                            Id = 2,
                            OfferId = 5,
                            Quantity = 1,
                            BillId = 1
                        },
                        new ArticleBill
                        {
                            Id = 3,
                            OfferId = 1,
                            Quantity = 1,
                            BillId = 2
                        },
                        new ArticleBill
                        {
                            Id = 4,
                            OfferId = 6,
                            Quantity = 2,
                            BillId = 2
                        },
                        new ArticleBill
                        {
                            Id = 5,
                            OfferId = 5,
                            Quantity = 1,
                            BillId = 2
                        },
                        new ArticleBill
                        {
                            Id = 6,
                            OfferId = 8,
                            Quantity = 1,
                            BillId = 3
                        },
                        new ArticleBill
                        {
                            Id = 7,
                            OfferId = 1,
                            Quantity = 12,
                            BillId = 3
                        },
                        new ArticleBill
                        {
                            Id = 8,
                            OfferId = 9,
                            Quantity = 1,
                            BillId = 3
                        },
                        new ArticleBill
                        {
                            Id = 9,
                            OfferId = 12,
                            Quantity = 1,
                            BillId = 4
                        },
                        new ArticleBill
                        {
                            Id = 10,
                            OfferId = 9,
                            Quantity = 1,
                            BillId = 4
                        },
                        new ArticleBill
                        {
                            Id = 11,
                            OfferId = 12,
                            Quantity = 1,
                            BillId = 6
                        },
                        new ArticleBill
                        {
                            Id = 12,
                            OfferId = 1,
                            Quantity = 2,
                            BillId = 6
                        }

                    }
                );

            builder.Entity<ServiceBill>()
                .HasData(new List<ServiceBill>
                {
                    new ServiceBill
                    {
                        Id = 1,
                        OfferId = 2,
                        EmployeeId = 1,
                        Duration = 3,
                        StartTime = DateTime.Parse("20.1.2021. 9:30"),
                        BillId = 2
                    },
                    new ServiceBill
                    {
                        Id = 2,
                        OfferId = 7,
                        EmployeeId = 1,
                        Duration = 2,
                        StartTime = DateTime.Parse("23.1.2021. 9:00"),
                        BillId = 5
                    },
                    new ServiceBill
                    {
                        Id = 3,
                        OfferId = 2,
                        EmployeeId = 2,
                        Duration = 4,
                        StartTime = DateTime.Parse("20.1.2021. 8:30"),
                        BillId = 7
                    },
                    new ServiceBill
                    {
                        Id = 4,
                        OfferId = 11,
                        EmployeeId = 3,
                        Duration = 1,
                        StartTime = DateTime.Parse("28.1.2021. 17:30"),
                        BillId = 7
                    }
                });

            builder.Entity<SubscriptionBill>()
                .HasData(new List<SubscriptionBill>
                    {
                        new SubscriptionBill()
                        {
                            Id = 1,
                            OfferId = 4,
                            CustomerId = 1,
                            StartTime = DateTime.Parse("2.7.2020. 11:11:12"),
                            BillId = null
                        },
                        new SubscriptionBill()
                        {
                            Id = 2,
                            OfferId = 10,
                            CustomerId = 1,
                            StartTime = DateTime.Parse("6.7.2020. 13:14:15"),
                            BillId = null
                        },
                        new SubscriptionBill()
                        {
                            Id = 3,
                            OfferId = 10,
                            CustomerId = 2,
                            StartTime = DateTime.Parse("1.9.2020. 13:00:07"),
                            BillId = null
                        },
                        new SubscriptionBill()
                        {
                            Id = 4,
                            OfferId = 3,
                            CustomerId = 3,
                            StartTime = DateTime.Parse("1.11.2020. 18:12:22"),
                            BillId = 8
                        }
                    }
                );

            builder.Entity<Bill>()
                .HasData(new List<Bill>
                    {
                        new Bill
                        {
                            Id = 1,
                            Type = BillType.Traditional,
                            TransactionDate = DateTime.Parse("30.12.2020. 12:00:01"),
                            Cost = 5.46m
                        },
                        new Bill
                        {
                            Id = 2,
                            Type = BillType.Traditional,
                            TransactionDate = DateTime.Parse("19.1.2021. 9:28:17"),
                            Cost = 124.83m
                        },
                        new Bill
                        {
                            Id = 3,
                            Type = BillType.Traditional,
                            TransactionDate = DateTime.Parse("19.1.2021. 9:31:33"),
                            Cost = 2517.87m
                        },
                        new Bill
                        {
                            Id = 4,
                            Type = BillType.Traditional,
                            TransactionDate = DateTime.Parse("19.1.2021. 10:21:34"),
                            Cost = 1880.00m
                        },
                        new Bill
                        {
                            Id = 5,
                            Type = BillType.Service,
                            TransactionDate = DateTime.Parse("19.1.2021. 14:00:01"),
                            Cost = 16.00m
                        },
                        new Bill
                        {
                            Id = 6,
                            Type = BillType.Traditional,
                            TransactionDate = DateTime.Parse("20.1.2021. 10:21:33"),
                            Cost = 81.49m
                        },
                        new Bill
                        {
                            Id = 7,
                            Type = BillType.Traditional,
                            TransactionDate = DateTime.Parse("20.1.2021. 10:25:00"),
                            Cost = 184.95m
                        },
                        new Bill
                        {
                            Id = 8,
                            Type = BillType.Subscription,
                            TransactionDate = DateTime.Parse("24.1.2021. 20:39:03"),
                            Cost = 210.00m
                        }
                    }
                );
        }
    }
}
