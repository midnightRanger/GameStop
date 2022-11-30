using GameStop.DAL.Interface;
using GameStop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStop.DAL.Repository;

public class ProductRepository : IProduct
{
    private ApplicationContext _db = new();

    public ProductRepository(ApplicationContext db)
    {
        _db = db; 
    }

    
    public async Task addProduct(ProductModel product)
    {
        try
        {
            _db.Product.Add(product);
            await _db.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }

    public void updateProduct(ProductModel product)
    {
        try
        {
            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();
        }
        catch
        {
            throw;
        }
    }

    public ProductModel deleteProduct(in int id)
    {
        try
        {
            ProductModel? product = _db.Product.Find(id);

            if (product != null)
            {
                _db.Product.Remove(product);
                _db.SaveChanges();
                return product;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        catch
        {
            throw;
        }
    }
    
    public bool checkProduct(int id)
    {
        return _db.Product.Any(e => e.Id == id);
    }

    public async Task<List<ProductModel>> getProducts()
    {
        try
        {
            return await _db.Product.ToListAsync();
        }
        catch
        {
            throw; 
        }
    }

    public ProductModel getProduct(int? id)
    {
        try
        {
            ProductModel? product = _db.Product.Find(id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        catch
        {
            throw;
        }
    }

    public IQueryable<ProductModel> getAll()
    {
        return _db.Product; 
    }
}