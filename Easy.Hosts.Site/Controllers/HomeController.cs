﻿using Easy.Hosts.Site.App_Start;
using Easy.Hosts.Site.Models;
using Easy.Hosts.Site.Models.Enums;
using Easy.Hosts.Site.Models.ViewModel;
using System;
using System.Collections.Generic;
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
            SiteViewModel siteViewModel = new SiteViewModel();
            ViewBag.BedroomId = new SelectList(db.Bedroom, "Id", "NameBedroom");
            return View(siteViewModel);
        }

        public ActionResult Quartos()
        {
            var bedroom = db.Bedroom.Include(i => i.TypeBedroom).Where(w => w.Status == BedroomStatus.Disponivel).ToList();
            return View(bedroom);
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
            User user = await db.User.Where(t => t.Email == access.Email && t.Password == passcrip)
                .Where(w => w.Status == 1)
                .FirstOrDefaultAsync();

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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Register register)
        {
            if (ModelState.IsValid)
            {
                if (db.User.Where(x => x.Email == register.Email).ToList().Count > 0)
                {
                    TempData["MSG"] = "warning|E-mail já existe, tente novamente com outro!";
                    return View("Index",register);
                }

                User user = new User();
                user.Name = register.Name;
                user.Email = register.Email;
                user.Password = Functions.HashText(register.Password, "SHA512");
                user.ConfirmPassword = Functions.HashText(register.ConfirmPassword, "SHA512");
                user.Cpf = register.Cpf;
                user.Status = 0;
                user.Perfil = db.Perfil.Find(2);
                user.Hash = Functions.Encode(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss.ffff"));


                if (user.Perfil == null)
                {
                    TempData["MSG"] = "warning|Não existe o perfil para cadastro!";
                    return View(register);
                }

                string msg = "<h3>Easy Hosts</h3>";
                msg += "Para confirmar sua conta acesse <a href='https://localhost:44301/Home/ConfirmarConta/" + user.Hash + "'target = '_blank'>clique aqui</a>";
                Functions.SendEmail(user.Email, "Confirmacao de conta criada no Easy Hosts", msg);

                db.User.Add(user);
                await db.SaveChangesAsync();
                TempData["MSG"] = "success|Conta criada com sucesso, acesse sua caixa de email para ativacao de conta!";
                return RedirectToAction("Index");
            }
            TempData["MSG"] = "error|Erro ao criar conta, verifique os campos e tente novamente!";
            return View(register);
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
            if (ModelState.IsValid)
            {
                if (dateCheckin == null)
                {
                    TempData["MSG"] = "info|Preencha a data de checkin";
                    return RedirectToAction("Index");
                }
                Bedroom bedroom = db.Bedroom.Where(w => w.Id == bedroomId).FirstOrDefault();

                booking.CodeBooking = Functions.CodeBookigSort();
                booking.Status = BookingStatus.Voucher;
                booking.DateCheckin = dateCheckin.AddHours(14);
                booking.DateCheckout = dateCheckout.AddHours(12);
                booking.UserId = userId;
                booking.BedroomId = bedroom.Id;
                booking.ValueBooking = Functions.QuantityDaysBooking(dateCheckin, dateCheckout, bedroom.Value);

                db.Booking.Add(booking);
                await db.SaveChangesAsync();

                bedroom.Status = BedroomStatus.Reservado;
                db.Entry(bedroom).State = EntityState.Modified;
                db.SaveChanges();

                User user = db.User.Where(x => x.Id == booking.UserId).FirstOrDefault();


                string msg = "<h3>RESERVA DO SITE EASY HOSTS</h3>";
                msg += "Link para pagamento:  <a href='https://localhost:44348/' target='_blank'>Pagar</a>";
                msg += "Codigo para o checkin" + booking.CodeBooking;
                Functions.SendEmail(user.Email, "Link de pagamento da reserva ", msg);

                TempData["MSG"] = "success|Reserva realizada com sucesso, verifique seu email o link de pagamento";
                return RedirectToAction("Index");
            }

            TempData["MSG"] = "info|Erro ao fazer a reserva, verifique os campos e tente novamente";
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


        //GET: PEGA O ID DO QUARTO E CONVERTE OS BYTES DO BANCO PARA O TIPO 'image/jpeg'
        public FileContentResult GetImageBedroom(int id)
        {
            byte[] byteArray = db.Bedroom.Find(id).Picture;

            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
        }

        public FileContentResult GetImageEvent(int id)
        {
            byte[] byteArray = db.Event.Find(id).Picture;

            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
        }

        public ActionResult ConfirmarConta(string hash)
        {
            if (!String.IsNullOrEmpty(hash))
            {
                var user = db.User.Where(x => x.Hash == hash).ToList().FirstOrDefault();
                if (user != null)
                {
                    try
                    {
                        DateTime dt = Convert.ToDateTime(Functions.Decode(user.Hash));
                        if (dt > DateTime.Now)
                        {
                            AtivarConta active = new AtivarConta();
                            active.Hash = user.Hash;
                            active.Email = user.Email;
                            return View(active);
                        }
                        TempData["MSG"] = "warning|Esse link já expirou!";
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        TempData["MSG"] = "error|Hash inválida!";
                        return RedirectToAction("Index");
                    }
                }
                TempData["MSG"] = "error|Hash inválida!";
                return RedirectToAction("Index");
            }
            TempData["MSG"] = "error|Acesso inválido!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarConta(AtivarConta ativarConta)
        {
            if (ModelState.IsValid)
            {
                User user = db.User.Where(x => x.Hash == ativarConta.Hash).ToList().FirstOrDefault();
                if (user != null)
                {
                    user.Hash = null;
                    user.Status = 1;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MSG"] = "success|Senha conta foi ativida com sucesso!";
                    return RedirectToAction("Index");
                }
                TempData["MSG"] = "error|E-mail não encontrado!";
                return View(ativarConta);
            }
            TempData["MSG"] = "warning|Preencha todos os campos!";
            return View(ativarConta);
        }



    }
}