﻿using PointOfSale.Models;
using System.Globalization;
using PointOfSale.Model;
using AutoMapper;


namespace PointOfSale.Utilities.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {

            #region Rol
            CreateMap<Rol, VMRol>().ReverseMap();
            #endregion

            #region User
            CreateMap<User, VMUser>()
            .ForMember(destiny =>
                destiny.IsActive,
                opt => opt.MapFrom(source => source.IsActive == true ? 1 : 0)
            )
            .ForMember(destiny =>
                destiny.NameRol,
                opt => opt.MapFrom(source => source.IdRolNavigation.Description)
            ).ForMember(destiny =>
                destiny.PhotoBase64,
                opt => opt.MapFrom(source => Convert.ToBase64String(source.Photo))
            )
            .ForMember(destiny =>
                destiny.Photo,
                opt => opt.Ignore()
            );

            CreateMap<VMUser, User>()
            .ForMember(destiny =>
                destiny.IsActive,
                opt => opt.MapFrom(source => source.IsActive == 1 ? true : false)
            )
            .ForMember(destiny =>
                destiny.IdRolNavigation,
                opt => opt.Ignore()
            );
            #endregion

            #region Category
            CreateMap<Category, VMCategory>()
            .ForMember(destiny =>
                destiny.IsActive,
                opt => opt.MapFrom(source => source.IsActive == true ? 1 : 0)
            );

            CreateMap<VMCategory, Category>()
            .ForMember(destiny =>
                destiny.IsActive,
                opt => opt.MapFrom(source => source.IsActive == 1 ? true : false)
            );
            #endregion

            #region Product
            CreateMap<Product, VMProduct>()
            .ForMember(destiny =>
                destiny.IsActive,
                opt => opt.MapFrom(source => source.IsActive == true ? 1 : 0)
            )
            .ForMember(destiny =>
                destiny.NameCategory,
                opt => opt.MapFrom(source => source.IdCategoryNavigation.Description)
            )
			.ForMember(destiny =>
				destiny.TaxDescription,
				opt => opt.MapFrom(source => source.IdTaxNavigation.Description)
			)
			.ForMember(destiny =>
                destiny.Price,
                opt => opt.MapFrom(source => Convert.ToString(source.Price.Value, new CultureInfo("es-PE")))
            )
            .ForMember(destiny =>
                destiny.TotalPrice,
                opt => opt.MapFrom(source => Convert.ToString(source.TotalPrice.Value, new CultureInfo("es-PE")))
            )
            .ForMember(destiny =>
                destiny.PhotoBase64,
                opt => opt.MapFrom(source => Convert.ToBase64String(source.Photo))
            );

            CreateMap<VMProduct, Product>()
            .ForMember(destiny =>
                destiny.IsActive,
                opt => opt.MapFrom(source => source.IsActive == 1 ? true : false)
            )
            .ForMember(destiny =>
                destiny.IdCategoryNavigation,
                opt => opt.Ignore()
            )
            .ForMember(destiny =>
                destiny.IdTaxNavigation,
                opt => opt.Ignore()
            )
            .ForMember(destiono =>
                destiono.Price,
                opt => opt.MapFrom(source => Convert.ToDecimal(source.Price, new CultureInfo("es-PE")))
            );
            #endregion

            #region TypeDocumentSale
            CreateMap<TypeDocumentSale, VMTypeDocumentSale>().ReverseMap();
            #endregion

            #region TypeDocumentPurchase
            CreateMap<TypeDocumentPurchase, VMTypeDocumentPurchase>().ReverseMap();
            #endregion

            #region Sale
            CreateMap<Sale, VMSale>()
                .ForMember(destiny =>
                    destiny.TypeDocumentSale,
                    opt => opt.MapFrom(source => source.IdTypeDocumentSaleNavigation.Description)
                )
                .ForMember(destiny =>
                    destiny.Users,
                    opt => opt.MapFrom(source => source.IdUsersNavigation.Name)
                )
                .ForMember(destiny =>
                    destiny.Subtotal,
                    opt => opt.MapFrom(source => Convert.ToString(source.Subtotal.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.TotalTaxes,
                    opt => opt.MapFrom(source => Convert.ToString(source.TotalTaxes.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.Total,
                    opt => opt.MapFrom(source => Convert.ToString(source.Total.Value, new CultureInfo("es-PE")))
                ).ForMember(destiny =>
                    destiny.RegistrationDate,
                    opt => opt.MapFrom(source => source.RegistrationDate.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<VMSale, Sale>()
                .ForMember(destiny =>
                    destiny.Subtotal,
                    opt => opt.MapFrom(source => Convert.ToDecimal(source.Subtotal, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.TotalTaxes,
                    opt => opt.MapFrom(source => Convert.ToDecimal(source.TotalTaxes, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.Total,
                    opt => opt.MapFrom(source => Convert.ToDecimal(source.Total, new CultureInfo("es-PE")))
                );
            #endregion
            
            #region Purchase
            CreateMap<Purchase, VMPurchase>()
                .ForMember(destiny =>
                    destiny.IdTypeDocumentPurchase,
                    opt => opt.MapFrom(source => source.IdTypeDocumentNavigation.Description)
                )
                .ForMember(destiny =>
                    destiny.IdUsers,
                    opt => opt.MapFrom(source => source.IdUsersNavigation.Name)
                )
                .ForMember(destiny =>
                    destiny.Subtotal,
                    opt => opt.MapFrom(source => Convert.ToString(source.Subtotal.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.TotalTaxes,
                    opt => opt.MapFrom(source => Convert.ToString(source.TotalTaxes.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.Total,
                    opt => opt.MapFrom(source => Convert.ToString(source.Total.Value, new CultureInfo("es-PE")))
                ).ForMember(destiny =>
                    destiny.RegistrationDate,
                    opt => opt.MapFrom(source => source.RegistrationDate.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<VMPurchase, Purchase>()
                .ForMember(destiny =>
                    destiny.Subtotal,
                    opt => opt.MapFrom(source => Convert.ToDecimal(source.Subtotal, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.TotalTaxes,
                    opt => opt.MapFrom(source => Convert.ToDecimal(source.TotalTaxes, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.Total,
                    opt => opt.MapFrom(source => Convert.ToDecimal(source.Total, new CultureInfo("es-PE")))
                );
            #endregion

            #region DetailSale
            CreateMap<DetailSale, VMDetailSale>()
                .ForMember(destiny =>
                    destiny.Price,
                    opt => opt.MapFrom(source => Convert.ToString(source.Price.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.Total,
                    opt => opt.MapFrom(source => Convert.ToString(source.Total.Value, new CultureInfo("es-PE")))
                );

            CreateMap<VMDetailSale, DetailSale>()
                .ForMember(destiny =>
                    destiny.Price,
                    opt => opt.MapFrom(source => Convert.ToDecimal(source.Price, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.Total,
                    opt => opt.MapFrom(source => Convert.ToDecimal(source.Total, new CultureInfo("es-PE")))
                );

            CreateMap<DetailSale, VMSalesReport>()
                .ForMember(destiny =>
                    destiny.RegistrationDate,
                    opt => opt.MapFrom(source => source.IdSaleNavigation.RegistrationDate.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destiny =>
                    destiny.SaleNumber,
                    opt => opt.MapFrom(source => source.IdSaleNavigation.SaleNumber)
                )
                .ForMember(destiny =>
                    destiny.DocumentType,
                    opt => opt.MapFrom(source => source.IdSaleNavigation.IdTypeDocumentSaleNavigation.Description)
                )
                .ForMember(destiny =>
                    destiny.DocumentClient,
                    opt => opt.MapFrom(source => source.IdSaleNavigation.CustomerDocument)
                )
                .ForMember(destiny =>
                    destiny.ClientName,
                    opt => opt.MapFrom(source => source.IdSaleNavigation.ClientName)
                )
                .ForMember(destiny =>
                    destiny.SubTotalSale,
                    opt => opt.MapFrom(source => Convert.ToString(source.IdSaleNavigation.Subtotal.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.TaxTotalSale,
                    opt => opt.MapFrom(source => Convert.ToString(source.IdSaleNavigation.TotalTaxes.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.Discount,
                    opt => opt.MapFrom(source => Convert.ToString(source.IdSaleNavigation.Discount.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.TotalSale,
                    opt => opt.MapFrom(source => Convert.ToString(source.IdSaleNavigation.Total.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.Product,
                    opt => opt.MapFrom(source => source.DescriptionProduct)
                )
                .ForMember(destiny =>
                    destiny.Price,
                    opt => opt.MapFrom(source => Convert.ToString(source.Price.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.Total,
                    opt => opt.MapFrom(source => Convert.ToString(source.Total.Value, new CultureInfo("es-PE")))
                );
            #endregion
            
            #region DetailPurchase
            CreateMap<DetailPurchase, VMDetailPurchase>()
                .ForMember(destiny =>
                    destiny.Price,
                    opt => opt.MapFrom(source => Convert.ToString(source.Price.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.Total,
                    opt => opt.MapFrom(source => Convert.ToString(source.Total.Value, new CultureInfo("es-PE")))
                );

            CreateMap<VMDetailPurchase, DetailPurchase>()
                .ForMember(destiny =>
                    destiny.Price,
                    opt => opt.MapFrom(source => Convert.ToDecimal(source.Price, new CultureInfo("es-PE")))
                );

            CreateMap<DetailPurchase, VMPurchasesReport>()
                .ForMember(destiny =>
                    destiny.RegistrationDate,
                    opt => opt.MapFrom(source => source.IdPurchaseNavigation.RegistrationDate.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destiny =>
                    destiny.PurchaseNumber,
                    opt => opt.MapFrom(source => source.IdPurchaseNavigation.PurchaseNumber)
                )
                .ForMember(destiny =>
                    destiny.DocumentType,
                    opt => opt.MapFrom(source => source.IdPurchaseNavigation.IdTypeDocumentNavigation.Description)
                )
                .ForMember(destiny =>
                    destiny.SellerDocument,
                    opt => opt.MapFrom(source => source.IdPurchaseNavigation.SellerDocument)
                )
                .ForMember(destiny =>
                    destiny.SellerName,
                    opt => opt.MapFrom(source => source.IdPurchaseNavigation.SellerName)
                )
                .ForMember(destiny =>
                    destiny.Subtotal,
                    opt => opt.MapFrom(source => Convert.ToString(source.IdPurchaseNavigation.Subtotal.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.TotalTaxes,
                    opt => opt.MapFrom(source => Convert.ToString(source.IdPurchaseNavigation.TotalTaxes.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.Discount,
                    opt => opt.MapFrom(source => Convert.ToString(source.IdPurchaseNavigation.Discount.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.Total,
                    opt => opt.MapFrom(source => Convert.ToString(source.IdPurchaseNavigation.Total.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.Category,
                    opt => opt.MapFrom(source => source.DescriptionCategory)
                )
                .ForMember(destiny =>
                    destiny.UnitPrice,
                    opt => opt.MapFrom(source => Convert.ToString(source.Price.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destiny =>
                    destiny.Total,
                    opt => opt.MapFrom(source => Convert.ToString(source.Total.Value, new CultureInfo("es-PE")))
                );
            #endregion

            #region Menu
            CreateMap<Menu, VMMenu>()
                .ForMember(destiny =>
                destiny.SubMenus,
                opt => opt.MapFrom(source => source.InverseIdMenuParentNavigation));
			#endregion Menu

			#region Tax
			CreateMap<VMTax, Tax>();
			CreateMap<Tax, VMTax>()
			.ForMember(destiny =>
				destiny.IsFixed,
				opt => opt.MapFrom(source => source.IsFixed == true ? 1 : 0)
			)

			.ForMember(destiny =>
				destiny.Description,
				opt => opt.MapFrom(source => source.Description)
			);
			#endregion
            
            #region Payment
			CreateMap<VMPayment, Payment>();
			CreateMap<Payment, VMPayment>()
			.ForMember(destiny =>
				destiny.IsActive,
				opt => opt.MapFrom(source => source.IsActive == true ? 1 : 0)
			)

			.ForMember(destiny =>
				destiny.Description,
				opt => opt.MapFrom(source => source.Description)
			);
			#endregion
		}
	}
}