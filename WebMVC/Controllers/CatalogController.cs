﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _service;
        public CatalogController(ICatalogService service) =>
            _service = service;
        
        public async Task<IActionResult> Index(int? brandFilterApplied,int? typeFilterApplied,int?page)
        {
            var itemsOnPage = 10;
            var catalog = await _service.GetCatalogItemsAsync(page ?? 0, itemsOnPage, brandFilterApplied, typeFilterApplied);

            var vm = new CatalogIndexViewModel
            {
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = 10,
                    TotalItems = catalog.Count,
                    TotalPages = (int)Math.Ceiling((decimal)catalog.Count / itemsOnPage)
                },
                CatalogItems = catalog.Data,
                Brands = await _service.GetBrandsAsync(),
                Types = await _service.GetTypesAsync(),
                BrandFilterApplied = brandFilterApplied ?? 0,
                TypesFilterApplied = typeFilterApplied ?? 0,
            };
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled":"";
            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            return View(vm);
        }
    }
}