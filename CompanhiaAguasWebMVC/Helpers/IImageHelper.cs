using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile formFile, string folder);
    }
}
