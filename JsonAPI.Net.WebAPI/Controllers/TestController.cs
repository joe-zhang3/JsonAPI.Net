using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using FlexibleJsonAPI.WebAPI.Resource;
using JsonAPI.Net;

namespace FlexibleJsonAPI.WebAPI.Controllers
{
    [RoutePrefix("test")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("account/{id:int}")]
        public Account GetAccount(int id){

            Account a = new Account() { 
                AccountId = 1, 
                FirstName="Joe", 
                LastName="Zhang", 
                State = new State(){StateId = 1, Name="US"},
            };

            a.Meta.Add("key", "value");

            return a;
        }

		[HttpGet]
		[Route("account")]
        [JaAction(masterTemplate:"Master")]
        public IEnumerable<Account> GetAccounts()
		{
            List<Account> accounts = new List<Account>() { 
                new Account() { AccountId = 1, FirstName = "Joe", LastName = "Zhang"} ,
                new Account() { AccountId = 2, FirstName = "Jenner", LastName = "Wang" },
                new Account() { AccountId = 3, FirstName = "Joe", LastName = "Zhang" }
            };

            return accounts;
        }

		[HttpGet]
		[Route("accounts")]
		[JaAction(masterTemplate: "Master")]
		public JaDocument GetAccounts1()
		{
			List<Account> accounts = new List<Account>() {
				new Account() { AccountId = 1, FirstName = "Joe", LastName = "Zhang"} ,
				new Account() { AccountId = 2, FirstName = "Jenner", LastName = "Wang" },
				new Account() { AccountId = 3, FirstName = "Joe", LastName = "Zhang" }
			};

            JaDocument jd = new JaDocument(accounts);

            jd.Meta.Add("total-counts", accounts.Count);

			return jd;
		}
    }
}
