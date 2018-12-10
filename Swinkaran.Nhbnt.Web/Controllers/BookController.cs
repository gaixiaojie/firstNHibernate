using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Linq;
using Swinkaran.Nhbnt.Web.Models;

namespace Swinkaran.Nhbnt.Web.Controllers
{
    public class BookController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "应用说明";
            IList<Book> books;
            using (ISession session = NhibernateSession.OpenSession())
            {
                books = session.Query<Book>().ToList();

            }
            return View(books);
        }
        public ActionResult Details(int id)
        {
            Book book = new Book();
            using (ISession session = NhibernateSession.OpenSession())
            {
                book = session.Query<Book>().Where(b => b.Id == id).FirstOrDefault();
            }
            return View(book);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Book book = new Book();
                book.Id = 115;
                book.Title= collection["Title"].ToString();
                book.Genre = collection["Genre"].ToString();
                book.Author = collection["Author"].ToString();
                using (ISession session = NhibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(book);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            Book book = new Book();
            using (ISession session = NhibernateSession.OpenSession())
            {
                book = session.Query<Book>().Where(b => b.Id == id).FirstOrDefault();

            }
            ViewBag.submitAction = "Save";
            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(int id,FormCollection collection)
        {
            try
            {
                Book book = new Book();
                book.Id = id;
                book.Title = collection["Title"].ToString();
                book.Genre = collection["Genre"].ToString();
                book.Author = collection["Author"].ToString();
                using (ISession session = NhibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(book);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            Book book = new Book();
            using (ISession session = NhibernateSession.OpenSession())
            {
                book = session.Query<Book>().Where(b => b.Id == id).FirstOrDefault();

            }
            ViewBag.SubmitAction = "Confirm delete";
            return View("Edit", book);
        }
        [HttpPost]
        public ActionResult Delete(long id,FormCollection collection)
        {
            try
            {
                using (ISession session = NhibernateSession.OpenSession())
                {
                    Book book = session.Get<Book>(id);
                    using(ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(book);
                        trans.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View();
            }

        }
    }
}