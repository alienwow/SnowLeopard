using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnowLeopard.Infrastructure;
using SnowLeopard.Infrastructure.Http;
using SnowLeopard.Model.BaseModels;
using SnowLeopard.WebApi.MongoEntities;
using SnowLeopard.Mongo;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using MongoDB.Driver;

namespace SnowLeopard.WebApi.Controllers
{
    /// <summary>
    /// Values
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VisitorsController : BaseApiController
    {
        private readonly ILogger _logger;
        private readonly SnowLeopardHttpClient _snowLeopardHttpClient;
        private readonly VistorMongoContext _vistorMongoCtx;

        public VisitorsController(
            ILogger<VisitorsController> logger,
            SnowLeopardHttpClient snowLeopardHttpClient,
            VistorMongoContext vistorMongoCtx
        )
        {
            _logger = logger;
            _vistorMongoCtx = vistorMongoCtx;
            _snowLeopardHttpClient = snowLeopardHttpClient;
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseDTO<IEnumerable<Visitor>>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<Visitor>> Get()
        {
            var result1 = await _vistorMongoCtx.Visitors.FindToListAsync(x => x.UserId == 0);
            return result1;
            //var result = await _snowLeopardHttpClient.GetAsync<string>("http://10.100.82.157:8013/api/v1/Health");
            //return await Task.FromResult(new string[] { "value1", "value2" });
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseDTO<Visitor>), (int)HttpStatusCode.OK)]
        public async Task<Visitor> Get(string id)
        {
            return await _vistorMongoCtx.Visitors.FirstOrDefaultAsync(x => x.Id == new MongoDB.Bson.ObjectId(id));
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public async Task Post([FromBody]Visitor value)
        {
            await _vistorMongoCtx.Visitors.InsertAsync(value);
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody] Visitor value)
        {
            value.Id = new MongoDB.Bson.ObjectId(id);
            await _vistorMongoCtx.Visitors.FindOneAndUpdateAsync(x => x.Id == value.Id, 
                Builders<Visitor>.Update
                .Set(x => x.UserId, value.UserId)
                .Set(x => x.VisitorId, value.VisitorId)
                .Set(x => x.VisitorTime, DateTime.Now)
            );
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _vistorMongoCtx.Visitors.DeleteOneAsync(x => x.Id == new MongoDB.Bson.ObjectId(id));
        }
    }
}
