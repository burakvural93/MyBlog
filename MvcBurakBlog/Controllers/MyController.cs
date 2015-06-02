
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBurakBlog.Models;
namespace MvcBurakBlog.Controllers
{
    public class MyController : Controller
    {
        // GET: My
        // private MvcProjesiContext db = new MvcProjesiContext();

        public ActionResult Index()
        {
            //var uyes = db.Uyes.ToList();
            return View();
        }
        //Son 5 makalenin ana sayfaya yükleneceği Action
        public ActionResult SonBesMakale()
        {
            //Veritabanından yeni bir nesne oluşturuyoruz.
            ApplicationDbContext db = new ApplicationDbContext();

            //Veritabanından sorgulamayı Linq ile yapıyoruz.
            //Tarih sırasına göre son makaleleri OrderByDescending ile çekip Take ile de 5 tane almasını istiyoruz.
            List<Makale> makaleListe = db.Makales.OrderByDescending(i => i.Tarih).Take(5).ToList();

            //Normal içeriklerde View döndürürken, burada ise PartialView döndürüyoruz.
            //Ayrıca makaleListe nesnesini de View'de kullanacağımız şekilde model olarak aktarıyoruz.
            return PartialView(makaleListe);
        }
        //Son 5 yorumun ana sayfaya yükleneceği Action
        public ActionResult SonBesYorum()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            //Tarih sırasına göre son makaleleri OrderByDescending ile çekip Take ile de 5 tane almasını istiyoruz.
            List<Yorum> yorumListe = db.Yorums.OrderByDescending(i => i.Tarih).Take(5).ToList();

            //Ayrıca yorumListe nesnesini de View'de kullanacağımız şekilde model olarak aktarıyoruz.
            return PartialView(yorumListe);
        }
        //En çok kullanılan 5 etiketin ana sayfaya yükleneceği Action
        public ActionResult EnCokOnEtiket()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            //Etiketleri sorgularken, kaç adet makaleye bağlandığını bulup, ona göre yüksekten,
            //aşağı doğru sıralanmasını sağlıyoruz. Gelen sonuçtan 10 adet alıp, listeye ekliyoruz.
            List<Etiket> etiketListe = (from i in db.Etikets orderby i.Makales.Count() descending select i).Take(10).ToList();

            //Ayrıca etiketListe nesnesini de View'de kullanacağımız şekilde model olarak aktarıyoruz.
            return PartialView(etiketListe);
        }
        public ActionResult TumMakaleler()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            //Tüm makalelerimizi, tarih sırasına göre, büyükten küçüğe olmak üzere çekiyoruz.
            List<Makale> makaleListe = (from i in db.Makales orderby i.Tarih descending select i).ToList();
            return View(makaleListe);
        }
        public ActionResult TumYorumlar()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<Yorum> yorumListe = (from i in db.Yorums orderby i.Tarih descending select i).ToList();
            return View(yorumListe);
        }
    }
}