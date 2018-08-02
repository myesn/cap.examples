using CAPExamples.EntityFramework.Abstractions;
using CAPExamples.EntityFramework.Abstractions.DbContexts;
using CAPExamples.EntityFramework.Abstractions.Entities;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPExamples.EntityFramework.Publisher.Host.Controllers
{
    public class PublishController : Controller
    {
        public PublishController(ICapPublisher publisher, AppDbContext dbContext)
        {
            _publisher = publisher;
            _dbContext = dbContext;
        }
        private readonly ICapPublisher _publisher;
        private readonly AppDbContext _dbContext;
        //private const string _topicName = "user.services.create";
        private readonly object _topicContent = new User("myesn", "developer");


        [HttpGet("~/publishUsingEF")]
        public async Task<IActionResult> PublishWithEntityFramework()
        {
            await _publisher.PublishAsync(TopicNames.UserCreate, _topicContent);

            return Ok("done");
        }

        [HttpGet("~/publishWithTransactionUsingEF")]
        public async Task<IActionResult> PublishMessageWithTransactionUsingEF()
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                await _publisher.PublishAsync(TopicNames.UserCreate, _topicContent);

                transaction.Commit();
            }

            return Ok("done");
        }
        
        [HttpGet("~/publishWithTransactionUsingAdonet")]
        public async Task<IActionResult> PublishMessageWithTransactionUsingAdonet()
        {
            return Ok("done");
        }

    }
}
