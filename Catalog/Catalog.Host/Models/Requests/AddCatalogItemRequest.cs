﻿namespace Catalog.Host.Models.Requests
{
    public class AddCatalogItemRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int AvailableStock { get; set; }
        public int CatalogBrandId { get; set; }
        public int CatalogTypeId { get; set; }
        public string? PictureFileName { get; set; }
    }
}