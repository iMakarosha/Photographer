using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using Photographer.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Photographer.Services
{
    public class PhotoService
    {
        ApplicationDbContext db = new ApplicationDbContext();
        string fullPath = HttpContext.Current.Server.MapPath("~/images/Photosession/");

        public int AddPhoto(System.Web.HttpFileCollectionBase files, string albumName)
        {
            int albumId = db.Albums.Where(a => a.Name == albumName).Select(a => a.Id).First();
            Photo photo = db.Photos.Add(new Photo
            {
                Date = DateTime.Now.ToShortDateString(),
                AlbumId = albumId
            });
            db.SaveChanges();
            UploadImage(files, photo.Id);
            return photo.Id;
        }

        public void DeletePhoto(int photoId)
        {
            Photo photo = db.Photos.Where(p => p.Id == photoId).First();
            if (photo != null)
            {
                db.Photos.Remove(photo);
                db.SaveChanges();
                DeleteImage(photoId);
            }
        }

        public IEnumerable<Photo> GetPhotos()
        {
            return db.Photos.Select(i => i).OrderByDescending(i => i.Id);
        }

        public IEnumerable<Photo> GetAlbumPhotos(int albumId, int count = 6)
        {
            return db.Photos.Select(a => a).Where(a=>a.AlbumId == albumId).Take(count).OrderByDescending(a => a.Id);
        }

        public int AddAlbum(string albumName, string categoryName)
        {
            int categoryId = db.Categories.Where(c => c.Name == categoryName).Select(c=>c.Id).First();
            int albumId = db.Albums.Add(new Album
            {
                Name = albumName,
                CategoryId = categoryId
            }).Id;
            db.SaveChanges();

            return albumId;
        }

        public Album GetAlbum(int albumId)
        {
            return db.Albums.Where(a => a.Id == albumId).FirstOrDefault();
        }

        public void SaveAlbum(Album album)
        {
            Album alb = db.Albums.Where(a => a.Id == album.Id).First();
            alb.Name = album.Name;
            alb.CategoryId = album.CategoryId;
            db.SaveChanges();
        }

        public void DeleteAlbum(int albumId)
        {
            Album album = db.Albums.Where(a => a.Id == albumId).First();
            db.Albums.Remove(album);
            db.SaveChanges();
        }

        public IEnumerable<Album> GetAlbums()
        {
            return db.Albums.Select(a => a).OrderByDescending(a => a.Id);
        }

        public int AddCategory(string categoryName)
        {
            int categoryId = db.Categories.Add(new Category
            {
                Name = categoryName
            }).Id;
            db.SaveChanges();

            return categoryId;
        }

        public void SaveCategory(Category category)
        {
            Category cat = db.Categories.Where(c => c.Id == category.Id).First();
            cat.Name = category.Name;
            db.SaveChanges();
        }

        public Category GetCategory(int categoryId)
        {
            return db.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
        }

        public void DeleteCategory(int categoryId)
        {
            Category cat = db.Categories.Where(c => c.Id == categoryId).First();
            db.Categories.Remove(cat);
            db.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            return db.Categories.Select(c => c).OrderBy(c => c.Name);
        }

        public string GetServiceName(int serviceId)
        {
            return db.Services.Where(s=>s.Id== serviceId).Select(s => s.Name).FirstOrDefault();
        }


        public IEnumerable<Service> GetServices()
        {
            return db.Services.Select(s => s).OrderBy(s => s.Id);
        }

        public void AddBooking(string name, string email, string phone, string comment, int serviceId)
        {

            db.Bookings.Add(new Booking
            {
                Name = name,
                Email = email,
                Phone = phone,
                Comment = comment,
                ServiceId = serviceId
            });
            db.SaveChanges();
        }

        public void DeleteBooking(int bookingId)
        {
            Booking booking = db.Bookings.Where(c => c.Id == bookingId).First();
            db.Bookings.Remove(booking);
            db.SaveChanges();
        }

        public IEnumerable<Booking> GetBookings()
        {
            return db.Bookings.Select(b => b).OrderBy(b => b.Id);
        }

        public PortfolioViewModel GetPortfolio()
        {
            return new PortfolioViewModel
            {
                categories = db.Categories.Select(c => new CatPortfolioViewModel()
                {
                    categoryName = c.Name,
                    albums = db.Albums.Where(a => a.CategoryId == c.Id).Select(a => new AlbPortfolioViewModel()
                    {
                        albumName = a.Name,
                        photosId = db.Photos.Where(p => p.AlbumId == a.Id).Select(p => p.Id).ToList()
                    }).ToList()
                }).OrderByDescending(c=>c.albums.Count).ToList()
            };

        }

        public void UploadImage(HttpFileCollectionBase files, int photoId)
        {
            var file = files[0];
            if (file != null)
            {
                System.Drawing.Bitmap bit;
                try
                {
                    //get filename
                    string fileName = System.IO.Path.GetFileName(file.FileName);
                    //save file
                    string fullPathName = fullPath + fileName;
                    file.SaveAs(fullPathName);
                    string newName = fullPath + photoId + "_original.jpg";
                    if (System.IO.File.Exists(newName))
                    {
                        System.IO.File.Delete(newName);
                    }

                    System.Drawing.Image imNormal = System.Drawing.Image.FromFile(fullPathName);
                    imNormal.Save(newName);

                    imNormal = ResizeImage(imNormal, 1920, 1280);
                    imNormal.Save(fullPath + photoId + "_md.jpg");

                    System.Drawing.Image imNormalSmall = ResizeImage(imNormal, 400, 270);
                    imNormalSmall.Save(fullPath + photoId + "_sm.jpg");
                    imNormal = null;
                    System.IO.File.Delete(fullPathName);
                }
                catch (Exception ex)
                {
                    string exeption = ex.Message;
                }
            }
        }

        public void DeleteImage(int photoId)
        {
            if (System.IO.File.Exists(fullPath + photoId + "_original.jpg"))
            {
                System.IO.File.Delete(fullPath + photoId + "_md.jpg");
                System.IO.File.Delete(fullPath + photoId + "_original.jpg");
                System.IO.File.Delete(fullPath + photoId + "_sm.jpg");
            }
        }

        public System.Drawing.Image ResizeImage(System.Drawing.Image img, int maxWidth, int maxHeight = 0)
        {
            if (maxHeight == 0) maxHeight = maxWidth;
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                Double xRatio = (double)img.Width / maxWidth;
                Double yRatio = (double)img.Height / maxHeight;
                Bitmap cpy = CommonBlock(img, xRatio, yRatio);
                return cpy;
            }
        }

        public System.Drawing.Bitmap CommonBlock(System.Drawing.Image img, double xRatio, double yRatio)
        {
            Double ratio = Math.Min(xRatio, yRatio);

            int nnx = (int)Math.Floor(img.Width / ratio);
            int nny = (int)Math.Floor(img.Height / ratio);
            Bitmap cpy = new Bitmap(nnx, nny, PixelFormat.Format32bppArgb);
            using (Graphics gr = Graphics.FromImage(cpy))
            {
                gr.Clear(Color.Transparent);

                // This is said to give best quality when resizing images
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;

                gr.DrawImage(img,
                    new Rectangle(0, 0, nnx, nny),
                    new Rectangle(0, 0, img.Width, img.Height),
                    GraphicsUnit.Pixel);
            }
            return cpy;
        }
    }
}