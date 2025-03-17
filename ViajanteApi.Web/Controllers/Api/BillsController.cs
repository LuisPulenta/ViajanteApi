using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using ViajanteApi.Common.Helpers;
using ViajanteApi.Web.Data;
using ViajanteApi.Web.Data.Entities;
using TicketsApi.Web.Models.Request;

namespace TicketsApi.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IFilesHelper _filesHelper;
        

        public BillsController(DataContext context, IFilesHelper filesHelper)
        {
            _context = context;
            _filesHelper = filesHelper;
                }

        //-----------------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBills()
        {
            List<Bill> bills = await _context.Bills
            .OrderBy(x => x.CreateDate)
              .ToListAsync();
            return Ok(bills);
        }

        //-----------------------------------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<Bill>> GetBilly(int id)
        {

            Bill bill = await _context.Bills
                .FirstOrDefaultAsync(p => p.Id == id);


            if (bill == null)
            {
                return NotFound();
            }

            return bill;
        }

        //-----------------------------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(int id, BillRequest billRequest)
        {
            if (id != billRequest.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Bill oldBill = await _context.Bills.FirstOrDefaultAsync(o => o.Id == billRequest.Id);

            //Foto
            string imageUrl = string.Empty;
            if (billRequest.ImageArray != null && billRequest.ImageArray.Length > 0)
            {
                imageUrl = string.Empty;
                var stream = new MemoryStream(billRequest.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Bills";
                var fullPath = $"~/images/Bills/{file}";
                var response = _filesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                    oldBill!.Photo = imageUrl;
                }
            }

            DateTime ahora = DateTime.Now;
            
            oldBill.Customer    = billRequest.Customer;
            oldBill .Type   = billRequest.Type;
            oldBill.Number  = billRequest.Number;
            oldBill.CreateDate = billRequest.CreateDate;
            oldBill.BillDate = billRequest.BillDate;
            oldBill.Amount = billRequest.Amount;
            oldBill.ChargeDate = billRequest.ChargeDate;
            oldBill.Charge = billRequest.Charge;
            oldBill.DeliverDate = billRequest.DeliverDate;
            oldBill.Deliver = billRequest.Deliver;
            oldBill.Photo = imageUrl;
            _context.Update(oldBill);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicada"))
                {
                    return BadRequest("Ya existe una factura/remito con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok(oldBill);
        }

        //-----------------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<Bill>> PostBill(BillRequest billRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime ahora = DateTime.Now;
            

            Bill newBill = new Bill
            {
                Id = 0,
                Customer = billRequest.Customer,
                Type = billRequest.Type,
                Number = billRequest.Number,
                CreateDate = ahora,
                BillDate = billRequest.BillDate,
                Amount = billRequest.Amount,
                ChargeDate = billRequest.ChargeDate,
                Charge = billRequest.Charge,
                DeliverDate = billRequest.DeliverDate,
                Deliver = billRequest.Deliver,
                Photo = null
            };

            //Foto
            if (billRequest.ImageArray != null)
            {
                var stream = new MemoryStream(billRequest.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Bills";
                var fullPath = $"~/images/Bills/{file}";
                var response = _filesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    newBill.Photo = fullPath;
                }
            }
            _context.Bills.Add(newBill);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(newBill);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicada"))
                {
                    return BadRequest("Ya existe esta Factura/Remito.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        //-----------------------------------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(int id)
        {
            Bill bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
