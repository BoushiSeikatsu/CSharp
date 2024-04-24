using Cviko8ASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cviko8ASPNET.Controllers
{
    public class EshopController : BaseController
    {
        private readonly ProductService productService;
        private readonly CartService cartService;

        public EshopController(ProductService productService, CartService cartService) : base(cartService)
        {
            this.productService = productService;
            this.cartService = cartService;
        }
        public IActionResult Index()
        {
            ViewBag.Products = productService.List();
            return View();
        }
        public IActionResult Detail(int id)
        {
            ViewBag.Product = productService.Find(id);
            return View();
        }
        public IActionResult Add(int id)
        {
            cartService.Add(productService.Find(id));
            return RedirectToAction("Detail", new { Id = id });
        }
        //Data jsou automaticky namapovana na ten form což je super
        //Pouzivame 2 stejne metody jedna s formem a ta se spusti jen pri odeslani httpPost jinak se pousti ta bez form
        [HttpPost]
        public IActionResult Form(OrderForm form)
        {
            //Pokud je validni pak presmeruj, jinak zobraz zase form
            if(ModelState.IsValid)
            {
                if(form.Quantity % 2 == 0)
                {
                    ModelState.AddModelError("Quantity", "Nesmí být sudý");//Taky se daji takhle resit kontroly, jinak bychom si museli zdedit IsValid metodu (pres jeji tridu nevim jakou)
                }
                return RedirectToAction("Done");
            }
            ViewBag.CartProducts = cartService.GetProducts();
            return View();
        }
        public IActionResult Form()
        {
            ViewBag.CartProducts = cartService.GetProducts();
            return View();
        }
        public IActionResult Done()
        {
            return View();
        }
        public IActionResult JsonProducts(int limit)//Serializace do json
        {
            return new JsonResult(productService.List().Take(limit));
        }
        
    }
}
