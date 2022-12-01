using Easy.Hosts.Site.App_Start;
using Easy.Hosts.Site.Models;
using Easy.Hosts.Site.Models.Enums;
using Easy.Hosts.Site.Models.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Easy.Hosts.Site.Controllers
{
    public class HomeController : Controller
    {
        private Context db = new Context();
        public ActionResult Index()
        {
            ViewBag.BedroomId = new SelectList(db.Bedroom, "Id", "NameBedroom");
            //var listBedroom = db.Bedroom.ToList();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Access access, string ReturnUrl)
        {
            string passcrip = Functions.HashText(access.Password, "SHA512");
            User user = db.User.Where(t => t.Email == access.Email && t.Password == passcrip)
                .Where(w => w.Status == 1)
                .FirstOrDefault();

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Id + "|" + user.Name, false);
                TempData["MSG"] = "success|Logado com Sucesso!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Email ou Senha inválidos!");
                TempData["MSG"] = "warning|Email ou Senha inválidos!";
                return View();
            }

            string permissoes = user.Perfil.Description;

            permissoes = permissoes.Substring(0, permissoes.Length - 1);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Id + "|" + user.Email, DateTime.Now, DateTime.Now.AddMinutes(30), false, permissoes);
            string hash = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
            Response.Cookies.Add(cookie);

        }
        public async Task<ActionResult> Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<ActionResult> MeuPerfil(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            User user = await db.User.FindAsync(id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public async Task<ActionResult> Eventos()
        {
            var listEvents = await db.Event.ToListAsync();
            return View(listEvents);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Booking(Booking booking, int userId, DateTime dateCheckin, DateTime dateCheckout, int bedroomId)
        {
            booking.CodeBooking = Functions.CodeBookigSort();
            booking.Status = BookingStatus.Voucher;
            booking.DateCheckin = dateCheckin;
            booking.DateCheckout = dateCheckout;
            booking.UserId = userId;
            booking.BedroomId = bedroomId;
            booking.ValueBooking = Functions.QuantityDaysBooking(dateCheckin, dateCheckout, 250);

            //booking.Bedroom.Status = BedroomStatus.Reservado;

            //db.Entry(booking).State = EntityState.Modified;
            await db.SaveChangesAsync();

            //var user = db.User.Where(x => x.Id == booking.UserId).FirstOrDefault();

            //string msg = "<h3>RESERVA DO SITE EASY HOSTS</h3>";
            //msg += "Link para pagamento:  <a href='https://localhost:44348/' target='_blank'>Pagar</a>";
            //msg += "Codigo para o checkin" + booking.CodeBooking;
            //Functions.SendEmail(user.Email, "Link de pagamento da reserva ", msg);



            TempData["MSG"] = "success|Reserva realizada com sucesso, segue em seu email o link de pagamento";
            return RedirectToAction("Index");
        }

        public ActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
            };
            return View(viewModel);
        }


    }
}