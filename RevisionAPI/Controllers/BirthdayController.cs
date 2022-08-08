using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Revision_API.Data;
using Revision_API.Models;

namespace Revision_API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BirthdayController : ControllerBase
    {
        private readonly Revision_APIContext _context;

        public BirthdayController(Revision_APIContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetBirthdays")]
        public Birthday GetBirthday(string name)
        {
            var birthday = _context.Birthdays
                .FirstOrDefault(birthday => birthday.Name.Equals(name));

            if (birthday is null)
                return null;

            var images = _context.Images.Where(x => x.BirthdayId == birthday.Id).ToList();
            birthday.Images = images;
            return birthday;
        }

        [HttpPost(Name = "PostBirthday")]
        public void PostBirthday(string name, DateTime date)
        {
            if (_context.Birthdays.Any(birthday => birthday.Name.Equals(name)))
                return;

            var birthday = new Birthday
            {
                Name = name,
                BirthdayDateTime = date,
                Images = new List<Image>()
            };
            _context.Birthdays.Add(birthday);
            _context.SaveChanges();
        }

        [HttpPost(Name = "PostBirthdayImage")]
        public void PostBirthdayImage(string name, string base64Image)
        {
            var birthday = _context.Birthdays.FirstOrDefault(birthday => birthday.Name.Equals(name));

            if (birthday is null)
                return;

            var image = new Image
            {
                Base64Image = base64Image,
                BirthdayId = birthday.Id
            };
            _context.Images.Add(image);
            _context.SaveChanges();
        }

        [HttpGet(Name = "UploadPictures")]
        public bool UploadPictures(string name)
        {

            foreach (var filePath in Directory.GetFiles(@"C:\Users\maxmalvins\OneDrive - Centiro Solutions AB\Skrivbordet\Pictures"))
            {
                using var image = System.Drawing.Image.FromFile(filePath);
                using var m = new MemoryStream();
                
                image.Save(m, image.RawFormat);
                var imageBytes = m.ToArray();

                var base64String = Convert.ToBase64String(imageBytes);
                PostBirthdayImage(name, base64String);
            }

            return true;
        }

        [HttpDelete(Name = "DeletePictures")]
        public void DeletePictures()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}
