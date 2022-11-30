using GameStop.DAL.Interface;
using GameStop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStop.DAL.Repository;

public class ProductInfoRepository : IProductInfo
{
    private ApplicationContext _db = new();

    public ProductInfoRepository(ApplicationContext db)
    {
        _db = db; 
    }

    
    public async Task addProductInfo(ProductInfoModel productInfo)
    {
        try
        {
            _db.ProductInfo.Add(productInfo);
            await _db.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }

    public void updateProductInfo(ProductInfoModel productInfo)
    {
        try
        {
            _db.Entry(productInfo).State = EntityState.Modified;
            _db.SaveChanges();
        }
        catch
        {
            throw;
        }
    }

    public ProductInfoModel deleteProductInfo(in int id)
    {
        try
        {
            ProductInfoModel? productInfo = _db.ProductInfo.Find(id);

            if (productInfo != null)
            {
                _db.ProductInfo.Remove(productInfo);
                _db.SaveChanges();
                return productInfo;
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
    
    public bool checkProductInfo(int id)
    {
        return _db.ProductInfo.Any(e => e.Id == id);
    }

    public async Task<List<ProductInfoModel>> getProductInfos()
    {
        try
        {
            return await _db.ProductInfo.ToListAsync();
        }
        catch
        {
            throw; 
        }
    }

    public async Task<ProductInfoModel> getProductInfo(int id)
    {
        try
        {
            ProductInfoModel? productInfo = await _db.ProductInfo.FindAsync(id);

            if (productInfo != null)
            {
                return productInfo;
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

    public IQueryable<ProductInfoModel> getAll()
    {
        return _db.ProductInfo; 
    }
}