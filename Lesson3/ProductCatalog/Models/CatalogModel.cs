using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ImgUrl { get; set; }
		public decimal Price { get; set; }
	}

	public class ProductAddingModel
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
		public string ImgUrl { get; set; }
		public decimal Price { get; set; }
	}

	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ConcurrentDictionary<int, Product> Products { get; set; }

		public Category() {
			Products = new ConcurrentDictionary<int, Product>();
		}

		public string AddProduct(Product np) => Products.TryAdd(np.Id, np) ? null : "������� � ����� ����� ��� ����������";

		public void DeleteProduct(int Id)
		{
			Products.TryRemove(Id, out _);
		}
	}

	public class CategoryAddingModel
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
	}

	public class Catalog
	{
		public ConcurrentDictionary<int, Category> Categories { get; private set; }

		static Catalog instance = null;

		public static Catalog GetCatalog()
		{
			if (instance == null) instance = new Catalog();
			return instance;
		}

		public Category GetCategory(int Id) => Categories[Id];

		public string AddCategory(Category nc) => Categories.TryAdd(nc.Id, nc) ? null : "��������� � ����� ����� ��� ����������";

		public void DeleteCategory(int Id)
		{
			Categories.TryRemove(Id, out _);
		}

		private Catalog() {
			Categories = new ConcurrentDictionary<int, Category>();
			AddTestData();
		}

		private void AddTestData()
		{
			Category c;
			c = new Category() { Id = 1, Name = "�������� �������" };
			c.AddProduct(new Product() { Id = 101, Name = "����", ImgUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ad/%D0%91%D0%B0%D1%82%D0%BE%D0%BD_%D0%A1%D0%BB%D0%BE%D0%B1%D0%BE%D0%B6%D0%B0%D0%BD%D1%81%D0%BA%D0%B8%D0%B9_%D0%A5%D0%B0%D1%80%D1%8C%D0%BA%D0%BE%D0%B2.JPG/800px-%D0%91%D0%B0%D1%82%D0%BE%D0%BD_%D0%A1%D0%BB%D0%BE%D0%B1%D0%BE%D0%B6%D0%B0%D0%BD%D1%81%D0%BA%D0%B8%D0%B9_%D0%A5%D0%B0%D1%80%D1%8C%D0%BA%D0%BE%D0%B2.JPG", Price = 40 });
			c.AddProduct(new Product() { Id = 102, Name = "������", ImgUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0e/Milk_glass.jpg/532px-Milk_glass.jpg", Price = 100 });
			AddCategory(c);
			c = new Category() { Id = 2, Name = "������� ��������" };
			c.AddProduct(new Product() { Id = 201, Name = "Nokia 3310", ImgUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/78/Nokia_3310_Blue_R7309170_%28retouch%29.png/263px-Nokia_3310_Blue_R7309170_%28retouch%29.png", Price = 5000 });
			c.AddProduct(new Product() { Id = 202, Name = "iPhone 13", ImgUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0c/IPhone_13_vector.svg/298px-IPhone_13_vector.svg.png", Price = 70000 });
			AddCategory(c);
		}
	}
}
