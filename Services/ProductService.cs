using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using ONLINE_SHOPPING_API.Models;
using ONLINE_SHOPPING_API.Repositories;

namespace ONLINE_SHOPPING_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _environment;

        private readonly string[] _allowedExtensions =
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        };

        public ProductService(
            IProductRepository productRepository,
            IWebHostEnvironment environment)
        {
            _productRepository = productRepository;
            _environment = environment;
        }


        // =========================
        // ADD PRODUCT
        // =========================

        public async Task<int> AddProduct(Product product)
        {
            string? imageName = null;

            if (product.ImageFile != null)
            {
                ValidateImage(product.ImageFile);

                imageName =
                    await SaveImage(product.ImageFile);

                product.PrdImage = imageName;
            }

            try
            {
                return await _productRepository.AddProduct(product);
            }
            catch
            {
                // If DB insert fails, remove uploaded image
                if (imageName != null)
                {
                    DeleteImage(imageName);
                }

                throw;
            }
        }


        // =========================
        // GET PRODUCT
        // =========================

        public async Task<Product?> GetProduct(int prdId)
        {
            return await _productRepository.GetProduct(prdId);
        }


        // =========================
        // GET ALL PRODUCTS
        // =========================

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }


        // =========================
        // UPDATE PRODUCT
        // =========================

        public async Task<bool> UpdateProduct(Product product)
        {
            var existingProduct =
                await _productRepository.GetProduct(product.PrdId);

            if (existingProduct == null)
                return false;

            string? oldImage = existingProduct.PrdImage;
            string? newImage = null;


            // New image selected
            if (product.ImageFile != null)
            {
                ValidateImage(product.ImageFile);

                newImage =
                    await SaveImage(product.ImageFile);

                product.PrdImage = newImage;
            }
            else
            {
                // Keep existing image
                product.PrdImage = oldImage;
            }


            try
            {
                var result =
                    await _productRepository.UpdateProduct(product);


                // DB update successful
                if (result)
                {
                    // Delete old image only if a new image was uploaded
                    if (newImage != null &&
                        !string.IsNullOrWhiteSpace(oldImage))
                    {
                        DeleteImage(oldImage);
                    }

                    return true;
                }


                // DB update failed
                if (newImage != null)
                {
                    DeleteImage(newImage);
                }

                return false;
            }
            catch
            {
                // DB exception → remove newly uploaded image
                if (newImage != null)
                {
                    DeleteImage(newImage);
                }

                throw;
            }
        }


        // =========================
        // DELETE PRODUCT
        // =========================

        public async Task<bool> DeleteProduct(int prdId)
        {
            var existingProduct =
                await _productRepository.GetProduct(prdId);

            if (existingProduct == null)
                return false;


            var result =
                await _productRepository.DeleteProduct(prdId);


            if (result)
            {
                // Delete physical image after DB delete
                if (!string.IsNullOrWhiteSpace(
                    existingProduct.PrdImage))
                {
                    DeleteImage(
                        existingProduct.PrdImage);
                }
            }


            return result;
        }


        // =========================
        // VALIDATE IMAGE
        // =========================

        private void ValidateImage(IFormFile image)
        {
            // Empty file
            if (image.Length <= 0)
            {
                throw new Exception(
                    "Please select a valid image.");
            }


            // Maximum 5 MB
            if (image.Length > 5 * 1024 * 1024)
            {
                throw new Exception(
                    "Image size cannot exceed 5 MB.");
            }


            // Extension
            var extension =
                Path.GetExtension(image.FileName)
                    .ToLowerInvariant();


            if (!_allowedExtensions.Contains(extension))
            {
                throw new Exception(
                    "Only JPG, JPEG, PNG and WEBP images are allowed.");
            }
        }


        // =========================
        // SAVE IMAGE
        // =========================

        private async Task<string> SaveImage(
            IFormFile image)
        {
            var extension =
                Path.GetExtension(image.FileName)
                    .ToLowerInvariant();


            // Unique filename
            var fileName =
                $"{Guid.NewGuid()}{extension}";


            // wwwroot path
            var webRootPath =
                _environment.WebRootPath;


            // Fallback if WebRootPath is null
            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                webRootPath =
                    Path.Combine(
                        _environment.ContentRootPath,
                        "wwwroot");
            }


            // wwwroot/images/products
            var folderPath =
                Path.Combine(
                    webRootPath,
                    "images",
                    "products");


            // Create folder if not exists
            Directory.CreateDirectory(folderPath);


            // Full file path
            var filePath =
                Path.Combine(
                    folderPath,
                    fileName);


            using var stream =
                new FileStream(
                    filePath,
                    FileMode.Create);


            await image.CopyToAsync(stream);


            return fileName;
        }


        // =========================
        // DELETE IMAGE
        // =========================

        private void DeleteImage(
            string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                return;


            var webRootPath =
                _environment.WebRootPath;


            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                webRootPath =
                    Path.Combine(
                        _environment.ContentRootPath,
                        "wwwroot");
            }


            var filePath =
                Path.Combine(
                    webRootPath,
                    "images",
                    "products",
                    imageName);


            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}