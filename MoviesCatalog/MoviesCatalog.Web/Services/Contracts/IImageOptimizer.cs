using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Services.Contracts
{
    public interface IImageOptimizer
    {
        string OptimizeImage(IFormFile inputImage, int endHeight, int endWidth);

        void DeleteOldImage(string imageName);
    }
}
