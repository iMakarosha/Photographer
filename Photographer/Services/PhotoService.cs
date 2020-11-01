using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using Photographer.Models;

namespace Photographer.Services
{
    public class PhotoService
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public int AddPhoto(int albumId)
        {
            int photoId = db.Photos.Add(new Photo
            {
                Date = DateTime.Now.ToShortDateString(),
                AlbumId = albumId
            }).Id;
            db.SaveChanges();

            return photoId;
        }

        public IEnumerable<Photo> GetPhotos()
        {
            return db.Photos.Select(i => i).OrderBy(i => i.Date);
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
            return db.Albums.Select(a => a).OrderBy(a => a.Name);
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

        public void AddService()
        {
            //int categoryId = db.Categories.Add(new Category
            //{
            //    Name = categoryName
            //}).Id;
            //db.SaveChanges();

            //return categoryId;
        }

        public IEnumerable<Service> GetServices()
        {
            return db.Services.Select(s => s).OrderBy(s => s.Name);
        }

        public void AddBooking()
        {
            //dafafd
        }

        public IEnumerable<Booking> GetBookings()
        {
            return db.Bookings.Select(b => b).OrderBy(b => b.Id);
        }
    }
}