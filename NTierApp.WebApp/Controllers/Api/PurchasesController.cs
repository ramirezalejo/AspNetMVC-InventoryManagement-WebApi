﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NTier.Domain;
using NTierApp.Business.Interface;
using NTierApp.WebApp.Models;

namespace NTierApp.WebApp.Controllers.Api
{
    [Authorize]
    public class PurchasesController : ApiController
    {
        private IPurchaseBusiness _purchase;

        public PurchasesController(IPurchaseBusiness purchase)
        {
            this._purchase = purchase;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var result = _purchase.GetAll().Select(x => new PurchaseViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UserId = x.UserId,
                PurchasedTime = x.PurchasedTime,
                Amount = x.Amount
            });
            return Ok(result);
        }
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var result = _purchase.GetById(id);

            if (result != null)
            {
                PurchaseViewModel vm = new PurchaseViewModel
                {
                    Id = result.Id,
                    ProductId = result.ProductId,
                    UserId = result.UserId,
                    PurchasedTime = result.PurchasedTime,
                    Amount = result.Amount
                };

                return Ok(vm);
            }
            return Ok("Item Not Found !");
        }
        [HttpPost]
        public IHttpActionResult Insert(PurchaseViewModel model)
        {
            Purchase purchase = new Purchase
            {
                Id = model.Id,
                ProductId = model.ProductId,
                UserId = model.UserId,
                Amount = model.Amount
            };
            _purchase.Insert(purchase);

            return Ok(purchase);
        }
        [HttpPut]
        public IHttpActionResult Update(PurchaseViewModel model)
        {
            Purchase purchase = new Purchase
            {
                Id = model.Id,
                ProductId = model.ProductId,
                UserId = model.UserId,
                Amount = model.Amount
            };
            _purchase.Update(purchase);
            return Ok(purchase);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _purchase.Delete(id);
            return Ok("Deleted Successfully !");
        }
    }
}
