using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cviko8ASPNET.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly CartService cartService;

        protected BaseController(CartService cart)
        {
            this.cartService = cart;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.CartProductCount = cartService.Count;
            base.OnActionExecuting(context);
        }
    }
}
