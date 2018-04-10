﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinancialPortal.Models;
using FinancialPortal.Models.Helpers;
using Microsoft.AspNet.Identity;

namespace FinancialPortal.Controllers
{
    public class InvitesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invites
        public ActionResult Index()
        {
            var invites = db.Invites.Include(i => i.Household);
            return View(invites.ToList());
        }

        // GET: Invites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invite invite = db.Invites.Find(id);
            if (invite == null)
            {
                return HttpNotFound();
            }
            return View(invite);
        }

        // GET: Invites/Create
        public ActionResult Create()
        {
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name");
            return View();
        }

        // POST: Invites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HouseholdId,Email,HHToken,InviteDate,InvitedById,HasBeenUsed")] Invite invite)
        {
            if (ModelState.IsValid)
            {
                GenerateToken token = new GenerateToken();
                Random rnd = new Random();
                string[] coupon = new string[10];
                for (int i = 0; i < coupon.Length; i++)
                {
                    coupon[i] = TokenGenerator(10, rnd);
                }

                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                invite.HouseholdId = user.HouseHoldId;
                invite.InviteDate = DateTime.Now;
                db.Invites.Add(invite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invite.HouseholdId);
            return View(invite);
        }

        // GET: Invites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invite invite = db.Invites.Find(id);
            if (invite == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invite.HouseholdId);
            return View(invite);
        }

        // POST: Invites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseholdId,Email,HHToken,InviteDate,InvitedById,HasBeenUsed")] Invite invite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invite.HouseholdId);
            return View(invite);
        }

        // GET: Invites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invite invite = db.Invites.Find(id);
            if (invite == null)
            {
                return HttpNotFound();
            }
            return View(invite);
        }

        // POST: Invites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invite invite = db.Invites.Find(id);
            db.Invites.Remove(invite);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
